using UnityEngine;

namespace BTVisual
{
    public class BehaviourTreeRunner : MonoBehaviour
    {
        public EnemyBrain brain;
        public BehaviourTree tree;

        private bool _active = false;
        public bool Active
        {
            get => _active;
            set => _active = value;
        }

        private void Start()
        {
            _active = true;
            var context = Context.CreateFromGameObject(gameObject);
#if UNITY_EDITOR
            tree = tree.Clone(); //복제해서 시작함.
            tree.Bind(context, brain); //만약 EnemyBrain과 같은 녀석을 여기서 바인드해서 넣어줘야 한다면 수정
#endif
        }

        private void Update()
        {
            if (_active)
                tree.Update();
        }
    }
}
