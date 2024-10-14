using UnityEngine;

namespace Factory
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe", order = 1)]
    public class Recipe : ScriptableObject
    {
        [SerializeField] private int _countMateriale;
        [SerializeField] private Material _materialForImprovement;

        [SerializeField] private int _countFurniture;
        [SerializeField] private Furniture _furnitureForImprovement;

        [SerializeField] private Furniture _furnitureFromRecipe;

        public int CountMateriale => _countMateriale;

        public Material MaterialForImprovement => _materialForImprovement;

        public int CountFurniture => _countFurniture;

        public Furniture FurnitureForImprovement => _furnitureForImprovement;

        public Furniture FurnitureFromRecipe => _furnitureFromRecipe;
    }
}