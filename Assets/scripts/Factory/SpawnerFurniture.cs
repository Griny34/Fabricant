using UnityEngine;

namespace Factory
{
    public class SpawnerFurniture : MonoBehaviour
    {
        [SerializeField] private Recipe _recipe;

        public Furniture CreateFurniture()
        {
            Furniture furniture = Instantiate(_recipe.FurnitureFromRecipe, transform.position, Quaternion.identity);

            return furniture;
        }

        public Recipe GetRecipe()
        {
            return _recipe;
        }
    }
}