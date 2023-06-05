namespace GameForum1.DAL
{
    public class Data
    {
        public static async Task<string> GenerateNickNameAsync()
        {
            var colors = "Yellow,Blue,Orange,Green,Red,Black,Purple,Gray,White,Pink,Brown".Split(',');
            string nickName = colors[Random.Shared.Next(colors.Length)];

            var dishes = "Spaghetti,Meatball,Pie,Potato,Maccaroni,Pizza,Broccoli,Ketchup,Cupcake,LemonChicken".Split(',');
            nickName += dishes[Random.Shared.Next(dishes.Length)];

            nickName += Random.Shared.Next(10, 1000);

            return nickName;
        }
    }
}
