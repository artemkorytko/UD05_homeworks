using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class TestTryCatch : MonoBehaviour
    {
        private Button _button;

        private void Start()
        {
            Debug.Log("Start(), start");
            
            try // отлавливать ошибки (т.е код теоретически может быть опастный, где может быть Exception)
            {
                _button = FindObjectOfType<Button>();
                _button.onClick.AddListener(ButtonClick); // = throw new NullReferenceException();
                                                          // на этом месте все остановиться не отработает Debug.Log("что-то");
                                                          
                Debug.Log("что-то");
                throw new PlayerDeadException(); // вызвать эту ошибку (ниже создана)
            }
            catch (ArgumentOutOfRangeException e) // если она произошла то сюда можно писать другую логику 
            {
                Debug.LogError($"Was ArgumentOutOfRangeException^ {e}");
            }
            catch (PlayerDeadException e)
            {
                Debug.LogError($"Was PlayerDeadException^ {e}");
            }
            catch (NullReferenceException e)
            {
                Debug.LogError($"Was NullReferenceException^ {e}");
            }
            catch (Exception e) // это должно идти последней(обязательно!) т.к NullReferenceException и др унаследованы от Exception и код будет не правильно отрабатывать
            {
                Debug.LogError($"Was exception^ {e}");
            }
            finally // всегда будет отрабатывать не в зависимости от исхода
            {
                Debug.Log("finally");
            }

            Debug.Log("Start(), end");
        }

        private void OnDestroy()
        {
            if(_button)
                _button.onClick.RemoveListener(ButtonClick);
        }

        private void ButtonClick()
        {
            throw new NotImplementedException();
        }
    }

    public class PlayerDeadException : Exception // создали свою ошибку
    {
        
    }
}