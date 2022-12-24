using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EmreBeratKR.DummyAds
{
    public abstract class BaseDummyAd : MonoBehaviour
    {
        private const float LoadDuration = 0.5f;

        private static readonly WaitForSecondsRealtime LoadTime = new WaitForSecondsRealtime(LoadDuration);


        [SerializeField] private GameObject mainAdObject;
        [SerializeField] private GameObject closeButton;
        [SerializeField] private Image adImage;
        
        
        public UnityAction OnOpened;
        public UnityAction OnLoaded;
        public UnityAction OnClosed;


        private string m_TargetUrl;


        protected virtual void Start()
        {
            Init();
        }
        

        public void OnAdClicked()
        {
            if (m_TargetUrl == null) return;
            
            if (m_TargetUrl == "") return;
            
            Application.OpenURL(m_TargetUrl);
        }
        
        public void OnCloseButtonClicked()
        {
            Close();
        }

        public BaseDummyAd SetAdSprite(Sprite sprite)
        {
            if (sprite == null) return this;
            
            adImage.sprite = sprite;

            return this;
        }

        public BaseDummyAd SetTargetUrl(string url)
        {
            m_TargetUrl = url;
            return this;
        }
        
        public virtual BaseDummyAd SetDuration(int duration)
        {
            return this;
        }

        protected void ShowCloseButton()
        {
            closeButton.SetActive(true);
        }


        private void Init()
        {
            mainAdObject.SetActive(false);
            closeButton.SetActive(false);
            Open();
        }
        
        private void Open()
        {
            OnOpened?.Invoke();
            Load();
        }

        private void Load()
        {
            StartCoroutine(Loading());
            
            IEnumerator Loading()
            {
                yield return LoadTime;
                mainAdObject.SetActive(true);
                OnLoaded?.Invoke();
            }
        }

        private void Close()
        {
            OnClosed?.Invoke();
            Destroy(gameObject);
        }
    }
}
