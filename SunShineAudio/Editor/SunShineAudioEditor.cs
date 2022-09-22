using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditorInternal;
using System;
namespace SunShineAudio
{
    public class SunShineAudioEditor : EditorWindow
    {
        public Texture2D bg;
        const float NORMAL_HEIGHT = 16;
        private string _outputFolder = string.Empty;
        private string _inputFolder = string.Empty;
        private Vector2 soundScrollViewOffset { get; set; }
        private Vector2 bgmScrollViewOffset { get; set; }
        private ReorderableList soundList { get; set; }
        private ReorderableList bgmList { get; set; }
        private List<AudioMessage> soundClipList { get; set; } = new List<AudioMessage>();
        private List<AudioMessage> bgmClipList { get; set; } = new List<AudioMessage>();
        private AudioList audioList;
        private static SunShineAudioEditor window;

        [MenuItem("Tools/Audio/AudioListMaker")]
        public static void ShowEditor()
        {
            window = EditorWindow.GetWindow<SunShineAudioEditor>("点歌单创建");
            window.minSize = new Vector2(960, 540);
            window.Show();
        }
        void OnEnable()
        {
            soundList = new ReorderableList(soundClipList, typeof(AudioMessage));
            soundList.drawHeaderCallback = DrawSoundHeader;
            soundList.drawElementCallback = DrawSoundElement;
            soundList.elementHeightCallback = GetElementHeight;
            bgmList = new ReorderableList(bgmClipList, typeof(AudioMessage));
            bgmList.drawHeaderCallback = DrawBgmHeader;
            bgmList.drawElementCallback = DrawBgmElement;
            bgmList.elementHeightCallback = GetElementHeight;

        }
        void OnGUI()
        {
            
            Rect welcomeImageRect = new Rect(0, 65,1920 , 1080);
            UnityEngine.GUI.DrawTexture(welcomeImageRect, bg);
            //选择音频的存储位置
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = false;
            _inputFolder = EditorGUILayout.TextField("音频存储文件夹", _inputFolder);
            GUI.enabled = true;
            if (GUILayout.Button("...", GUILayout.Width(40)))
            {
                _inputFolder = EditorUtility.SaveFolderPanel("音频存储文件夹", "Assets", string.Empty);
                if (!string.IsNullOrEmpty(_inputFolder))
                {
                    if (!_inputFolder.StartsWith(Application.dataPath))
                    {
                        _inputFolder = string.Empty;
                    }
                    else
                    {
                        _inputFolder = Path.Combine("Assets", GetRelativePath(_inputFolder, Application.dataPath));
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            //选择点歌单的存储位置
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = false;
            _outputFolder = EditorGUILayout.TextField("点歌单输出目录", _outputFolder);
            GUI.enabled = true;
            if (GUILayout.Button("...", GUILayout.Width(40)))
            {
                _outputFolder = EditorUtility.SaveFolderPanel("点歌单输出目录", "Assets", string.Empty);
                if (!string.IsNullOrEmpty(_outputFolder))
                {
                    if (!_outputFolder.StartsWith(Application.dataPath))
                    {
                        _outputFolder = string.Empty;
                    }
                    else
                    {
                        _outputFolder = Path.Combine("Assets", GetRelativePath(_outputFolder, Application.dataPath));
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = !string.IsNullOrEmpty(_inputFolder);
            if (GUILayout.Button("开始读取音频资源", GUILayout.Width(200)))
            {
                ReflashMusicList();
            }
            GUI.enabled = true;
            GUI.enabled = !string.IsNullOrEmpty(_outputFolder);
            if (GUILayout.Button("开始点歌单的制作", GUILayout.Width(200)))
            {
                StartMakeMusicList();
            }
            EditorGUILayout.EndHorizontal();
            GUI.enabled = true;

            bgmScrollViewOffset = EditorGUILayout.BeginScrollView(bgmScrollViewOffset);
            bgmList.DoLayoutList();
            EditorGUILayout.EndScrollView();

            soundScrollViewOffset = EditorGUILayout.BeginScrollView(soundScrollViewOffset);
            soundList.DoLayoutList();
            EditorGUILayout.EndScrollView();
        }
        string GetRelativePath(string filespec, string folder)
        {
            Uri pathUri = new Uri(filespec);
            // Folders must end in a slash
            if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                folder += Path.DirectorySeparatorChar;
            }
            Uri folderUri = new Uri(folder);
            return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
        }
        private void DrawSoundHeader(Rect rect)
        {
            GUI.Label(rect, "读取到的Sound列表");
        }
        private void DrawBgmHeader(Rect rect)
        {
            GUI.Label(rect, "读取到的Bgm列表");
        }
        private void DrawSoundElement(Rect rect, int index, bool active, bool focused)
        {
            AudioMessage audio = soundClipList[index];
            audio.audioClip = (AudioClip)EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, NORMAL_HEIGHT), "音频资源", audio.audioClip, typeof(AudioClip), false);
            audio.IsLoop = EditorGUI.Toggle(new Rect(rect.x, rect.y + NORMAL_HEIGHT*1.5f, rect.width, NORMAL_HEIGHT), "是否循环", audio.IsLoop);
            audio.volume = EditorGUI.Slider(new Rect(rect.x, rect.y + NORMAL_HEIGHT * 3f, rect.width, NORMAL_HEIGHT), "播放音量", audio.volume,0, 1);
            audio.maxNum = EditorGUI.IntField(new Rect(rect.x, rect.y + NORMAL_HEIGHT * 4.5f, rect.width, NORMAL_HEIGHT), "同时存在的最大数量", audio.maxNum);
        }
        private void DrawBgmElement(Rect rect, int index, bool active, bool focused)
        {
            AudioMessage audio = bgmClipList[index];
            audio.audioClip = (AudioClip)EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, NORMAL_HEIGHT), "音频资源", audio.audioClip, typeof(AudioClip), false);
            audio.IsLoop = EditorGUI.Toggle(new Rect(rect.x, rect.y + NORMAL_HEIGHT * 1.5f, rect.width, NORMAL_HEIGHT), "是否循环", audio.IsLoop);
            audio.volume = EditorGUI.Slider(new Rect(rect.x, rect.y + NORMAL_HEIGHT * 3f, rect.width, NORMAL_HEIGHT), "播放音量", audio.volume, 0, 1);
            audio.maxNum = EditorGUI.IntField(new Rect(rect.x, rect.y + NORMAL_HEIGHT * 4.5f, rect.width, NORMAL_HEIGHT), "同时存在的最大数量", audio.maxNum);
        }
        private float GetElementHeight(int index)
        {
            return NORMAL_HEIGHT * 6;
        }
        private void ReflashMusicList()
        {
            DirectoryInfo directory = new DirectoryInfo(_inputFolder);
            FileInfo[] files = directory.GetFiles("*",SearchOption.AllDirectories);
            foreach(FileInfo fileInfo in files)
            {
                if(fileInfo.Name.EndsWith(".meta"))
                {
                    continue;
                }
                string path = Path.Combine("Assets", GetRelativePath(fileInfo.FullName, Application.dataPath));
                if (fileInfo.DirectoryName.EndsWith("BGM"))
                {
                    AudioMessage audio = new AudioMessage()
                    {
                        audioClip = AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip)) as AudioClip,
                        IsLoop = false,
                        volume = 1,
                        maxNum = 5,
                   };
                    bgmClipList.Add(audio);
                }
                if (fileInfo.DirectoryName.EndsWith("Sound"))
                {
                    AudioMessage audio = new AudioMessage()
                    {
                        audioClip = AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip)) as AudioClip,
                        IsLoop = false,
                        volume = 1,
                        maxNum = 5,
                    };
                    soundClipList.Add(audio);
                }
            }
        }
        private void StartMakeMusicList()
        {
            audioList = new AudioList();
            foreach(AudioMessage item in bgmClipList)
            {
                audioList.bgmShowList.Add(item);
            }
            foreach (AudioMessage item in soundClipList)
            {
                audioList.soundShowList.Add(item);
            }
            CreateAudioList(audioList, GetOutputPath(_outputFolder,"AudioList.asset"));
        }
        private void CreateAudioList(AudioList audioList,string path)
        {
            AssetDatabase.CreateAsset(audioList, path);
        }
        public string GetOutputPath(string outputPath, string fileName)
        {
            return Path.Combine(outputPath, fileName);
        }
    }
}
