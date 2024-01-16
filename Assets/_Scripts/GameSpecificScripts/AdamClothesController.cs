public class AdamClothesController : BaseClothesController
{
    protected override void SetClothesTypeQueue()
    {
        clothesTypeQueue = "Beard - Body Style - Hair";
    }

    protected override void LoadAllClothes()
    {
        LoadClothe(clotheTypeOne, clotheTypeOneChosenIndex);
        LoadClothe(clotheTypeTwo, clotheTypeTwoChosenIndex);
        LoadClothe(clotheTypeThree, clotheTypeThreeChosenIndex);
    }
}
