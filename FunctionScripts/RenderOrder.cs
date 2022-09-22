using UnityEngine;



    public class RenderOrder : MonoBehaviour
    {

        private Renderer rd;
        public int sortingOrder;
        public string sortingLayer = "Default";

        public bool includeAllChilds;
        void Awake()
        {
            if (includeAllChilds)
            {
                Renderer[] rds = gameObject.GetComponentsInChildren<Renderer>();
                for (int i = 0; i < rds.Length; i++)
                {
                    rds[i].sortingOrder = sortingOrder;
                    rds[i].sortingLayerName = sortingLayer;
                }
            }
            else
            {
                rd = GetComponent<Renderer>();
                if (rd)
                {
                    rd.sortingOrder = sortingOrder;
                    rd.sortingLayerName = sortingLayer;
                }
            }
            Destroy(this);
        }

    }
