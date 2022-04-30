using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LeaderboardItem : MonoBehaviour
    {
        public Text indexText;
        public Text nameText;
        public Text ratingText;

        public void Init(int index, string playerName, int rating)
        {
            indexText.text = index.ToString();
            nameText.text = playerName;
            ratingText.text = rating.ToString();
        }
    }
}
