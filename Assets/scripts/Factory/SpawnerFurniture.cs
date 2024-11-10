using UnityEngine;

namespace Factory
{
    public class SpawnerFurniture : MonoBehaviour
    {
        [SerializeField] private Recipe _recipe;
        [SerializeField] private Transform _spawner;

        public Furniture CreateFurniture()
        {
            Furniture furniture = Instantiate(_recipe.FurnitureFromRecipe, _spawner.position, Quaternion.identity);

            return furniture;
        }

        public Recipe GetRecipe()
        {
            return _recipe;
        }
    }
}