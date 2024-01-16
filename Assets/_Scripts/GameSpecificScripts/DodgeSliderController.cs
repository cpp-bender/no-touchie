using UnityEngine.UI;
using UnityEngine;

public class DodgeSliderController : MonoBehaviour
{
    public bool isSliderHeldDown;
    public bool canPlay = false;

    public PlayerController player;
    private Slider slider;
    private float oldSliderValue;

    private void Start()
    {
        // player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();
        slider = GetComponent<Slider>();
        // player.dodgeSlider = slider;
        player.HandleDodgeSliderInitialValue();
    }

    public void SliderPointerDown()
    {
        isSliderHeldDown = true;
        oldSliderValue = slider.normalizedValue;
    }

    public void SliderPointerDropped()
    {
        //player.PlayDodgeAnimLittle(oldSliderValue);
    }

    public void SliderPointerUp()
    {
        isSliderHeldDown = false;
    }

    private void Update()
    {
        player.PlayDodgeAnim();

        #region OLD SLIDER LOGIC

        //if (isSliderHeldDown)
        //{
        //    player.PlayDodgeAnim();
        //}

        //else
        //{
        //    player.StopDodgeAnim();
        //}
        #endregion
    }
}
