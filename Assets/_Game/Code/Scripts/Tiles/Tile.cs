using NaughtyAttributes;
using UnityEngine;

namespace YNQ.JumpyJoe
{
	public class Tile : MonoBehaviour
	{
		[BoxGroup(Strings.BoxComponents), SerializeField] private Transform _jumpPos;
		[BoxGroup(Strings.BoxComponents), SerializeField] private Transform _obstaclePos;
		[BoxGroup(Strings.BoxComponents), SerializeField] private GameObject[] _randomObjects;
		[BoxGroup(Strings.BoxComponents), SerializeField] private Transform[] _propsTransforms;

		public Vector3 Pos => _jumpPos.position;
		public Vector3 ObstaclePos => _obstaclePos.position;
		public Transform[] PropsTransforms => _propsTransforms;

		public GameObject[] RandomObjects => _randomObjects;
    } 
}
