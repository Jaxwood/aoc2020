using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020.Lib.Day21
{
    public class IngredientMixer
    {
        private readonly IEnumerable<IngredientList> ingredients;

        public IngredientMixer(IEnumerable<IngredientList> ingredients)
        {
            this.ingredients = ingredients;
        }

        public long CountAllergens()
        {
            var allergens = this.GetListOfAllergens();
            var possibleAllergens = this.GetPossibleAllergen(allergens);

            var seen = new HashSet<string>();
            var queue = new Queue<string>(possibleAllergens.Keys);
            while (queue.Count > 0)
            {
                var allergen = queue.Dequeue();
                var ingredients = possibleAllergens[allergen];
                if (ingredients.Length == 1)
                {
                    seen.Add(ingredients[0]);
                }
                else
                {
                    var other = new HashSet<string>(ingredients);
                    other.ExceptWith(seen);
                    if (other.Count == 1)
                    {
                        seen.Add(other.First());
                    }
                    else
                    {
                        queue.Enqueue(allergen);
                    }
                }
            }

            return this.ingredients.SelectMany(c => c.Ingredients)
                                   .Where(i => !seen.Contains(i))
                                   .LongCount();
        }

        public string Allergens()
        {
            var allergens = this.GetListOfAllergens();
            var possibleAllergens = this.GetPossibleAllergen(allergens);

            var seen = new HashSet<string>();
            var confirmedAllergen = new Dictionary<string, string>();
            var queue = new Queue<string>(possibleAllergens.Keys);
            while (queue.Count > 0)
            {
                var allergen = queue.Dequeue();
                var ingredients = possibleAllergens[allergen];
                if (ingredients.Length == 1)
                {
                    seen.Add(ingredients[0]);
                    confirmedAllergen.Add(allergen, ingredients[0]);
                }
                else
                {
                    var other = new HashSet<string>(ingredients);
                    other.ExceptWith(seen);
                    if (other.Count == 1)
                    {
                        seen.Add(other.First());
                        confirmedAllergen[allergen] = other.First();
                    }
                    else
                    {
                        queue.Enqueue(allergen);
                    }
                }
            }

            return String.Join(",", confirmedAllergen.OrderBy(c => c.Key).Select(c => c.Value));
        }

        private Dictionary<string, string[]> GetPossibleAllergen(string[] allergens)
        {
            var possibleAllergens = new Dictionary<string, string[]>();

            foreach (var allergen in allergens)
            {
                var ingredients = this.GetListOfIngredientByAllergen(allergen);
                var set = new HashSet<string>(ingredients.First());
                foreach (var ingredientList in ingredients)
                {
                    var otherSet = new HashSet<string>(ingredientList);
                    set.IntersectWith(otherSet);
                }
                possibleAllergens[allergen] = set.ToArray();
            }

            return possibleAllergens;
        }

        private string[] GetListOfAllergens()
        {
            var allergens = new HashSet<string>(this.ingredients.SelectMany(i => i.Allergens));
            return allergens.ToArray();
        }

        private IEnumerable<IEnumerable<string>> GetListOfIngredientByAllergen(string allergen)
        {
            return this.ingredients.Where(c => c.Allergens.Contains(allergen))
                                   .Select(i => i.Ingredients);
        }
    }
}
