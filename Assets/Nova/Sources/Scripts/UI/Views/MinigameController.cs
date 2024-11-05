using System;
using System.Collections;
using UnityEngine.UI;

namespace Nova
{
    public class MinigameController : ViewControllerBase
    {
        private Text contentText;
        private Button cancelButton;

        //private ConfigManager configManager;
        private MinigameParameters param ;

        protected override void Awake()
        {
            base.Awake();

            contentText = myPanel.transform.Find("Background/Text").GetComponent<Text>();
            cancelButton = myPanel.transform.Find("Background/Button/Cancel").GetComponent<Button>();

            //configManager = Utils.FindNovaController().ConfigManager;

            I18n.LocaleChanged.AddListener(UpdateText);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            I18n.LocaleChanged.RemoveListener(UpdateText);
        }
        public void Minigame()
        {
            StartCoroutine(DoMinigame());
        }

        private IEnumerator DoMinigame()
        {
            yield return 4;

            contentText.gameObject.SetActive(true);
            UpdateText();
            cancelButton.gameObject.SetActive(true);

            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(() =>
            {
                this.Hide(null);
            });

            Show();
        }

        private void UpdateText()
        {
            contentText.text = I18n.__("ingame.title.minigame");
        }

        protected override void Update()
        {
            if (viewManager.currentView == CurrentViewType.Game)
            {
                OnActivatedUpdate();
            }
        }
    }
}
