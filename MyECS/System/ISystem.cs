namespace ECS.EcsSystem
{
    public interface ISystem
    {
        string name
        {
            get;
        }
        bool enabled
        {
            get;
        }

        void Update(float deltaTime);
        void LateUpdate(float deltaTime);

    }
}
