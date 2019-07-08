using System;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 },
                new[] { 2, 8 },
                new[] { 5, 2 },
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" },
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 },
                new[] { 2, 8, 5, 1 },
                new[] { 5, 2, 4, 4 },
                new[] { "tFc", "tF", "Ftc" },
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 },
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 },
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 },
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" },
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int l = protein.Length;
            int[][] nutritionGrid = new int[l][];

            GenerateNutritionGrid(ref nutritionGrid, l, protein, carbs, fat);

            int[][] sortedGrid = new int[l][];
            int[] menuSelection = new int[dietPlans.Length];

            for (int j = 0; j < dietPlans.Length; j++)
            {
                string diet = dietPlans[j];
                int[] col = { -1, -1, -1, -1 };
                for (int i = 0; i < diet.Length; i++)
                {
                    col[i] = GetIndex(diet[i]);
                }
                sortedGrid = getSortedGrid(nutritionGrid, col, diet);
                menuSelection[j] = sortedGrid[0][4];
            }

            return menuSelection;
        }

        private static int[][] getSortedGrid(int[][] nutritionGrid, int[] col, string diet)
        {
            return nutritionGrid.OrderBy(x => (col[0] != -1 ? Char.IsLower(diet[0]) ? x[col[0]] : -x[col[0]] : 0, col[1] != -1 ? Char.IsLower(diet[1]) ? x[col[1]] : -x[col[1]] : 0, col[2] != -1 ? Char.IsLower(diet[2]) ? x[col[2]] : -x[col[2]] : 0, col[3] != -1 ? Char.IsLower(diet[3]) ? x[col[3]] : -x[col[3]] : 0)).ToArray();
        }

        private static void GenerateNutritionGrid(ref int[][] nutritionGrid,int l,int[] protein, int[] carbs, int[] fat)
        {
            int calorie;
            for (int i = 0; i < l; i++)
            {
                calorie = (5 * protein[i]) + (5 * carbs[i]) + (9 * fat[i]);
                nutritionGrid[i] = new int[] { protein[i], carbs[i], fat[i], calorie, i };
            }
        }

        public static int GetIndex(char s)
        {
            int col;
            if (s.Equals('p') || s.Equals('P'))
            {
                col = 0;
            }
            else if (s.Equals('c') || s.Equals('C'))
            {
                col = 1;
            }
            else if (s.Equals('f') || s.Equals('F'))
            {
                col = 2;
            }
            else
            {
                col = 3;
            }
            return col;
        }
    }
}