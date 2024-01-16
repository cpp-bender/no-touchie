public class PlayerClothesController : BaseClothesController
{
    protected override void SetClothesTypeQueue()
    {
        clothesTypeQueue = "Hair - Dress - Shoes";
    }

    protected override void LoadAllClothes()
    {
        LoadClothe(clotheTypeOne, clotheTypeOneChosenIndex);
        LoadClothe(clotheTypeTwo, clotheTypeTwoChosenIndex);
        LoadClothe(clotheTypeThree, clotheTypeThreeChosenIndex);
    }
}
