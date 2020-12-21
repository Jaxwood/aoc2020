using System.Collections.Generic;

namespace Aoc2020.Lib.Day21
{
    public record IngredientList(IEnumerable<string> Ingredients, IEnumerable<string> Allergens);
}
