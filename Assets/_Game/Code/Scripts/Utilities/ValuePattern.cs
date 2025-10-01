using UnityEngine;

namespace YNQ.JumpyJoe
{
    [CreateAssetMenu(fileName = "ValuePattern", menuName = "Scriptable Objects/Value Pattern")]
    public class ValuePattern : ScriptableObject
    {
        [SerializeField] private string _valuePattern = "%Val%";

        [SerializeField, TextArea(2, 10)] private string _pattern;
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private bool _bold = false;
        [SerializeField] private bool _italic = false;

        public string Construct(object val)
        {
            var color = ColorUtility.ToHtmlStringRGB(_color);

            var output = $"<color=#{color}>";
            if (_bold)
                output += "<b>";
            if(_italic)
                output += "<i>";
            output += _pattern.Replace("%Val%", val.ToString());
            if(_italic)
                output += "</i>";
            if(_bold)
                output += "</b>";
            output += "</color>";

            return output;
        }
    }
}
