using System;
using Metro.StaticData;
using Metro.StaticData.Levels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Metro.Meta.Menu
{
    public class LevelCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI cardTitle;
        [SerializeField] private TextMeshProUGUI cardDescription;
        [SerializeField] private Toggle selectToggle;
        [SerializeField] private Image cardImage;

        public event Action<LevelStaticData> OnSelect; 

        public void Initialize(
            LevelStaticData staticData,
            // Sprite previewSprite,
            ToggleGroup toggleGroup)
        {
            cardTitle.text = staticData.Title;
            cardDescription.text = staticData.Description;
            // cardImage.sprite = previewSprite;
            
            selectToggle.onValueChanged.AddListener(arg =>
                {
                    OnSelect?.Invoke(arg
                        ? staticData 
                        : null
                    );
                }
            );
            selectToggle.group = toggleGroup;
        }
    }
}