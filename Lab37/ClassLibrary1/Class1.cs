namespace Lab37
{
    using System;

    // Класс пользовательского атрибута с свойством Comment
    public class CommentAttribute : Attribute
    {
        public string Comment { get; set; }

        public CommentAttribute(string comment)
        {
            Comment = comment;
        }
    }

    // Абстрактный класс Animal
    [Comment("Abstract base class for all animals")]
    public abstract class Animal
    {
        public string Name { get; set; }
        public string Country { get; set; }

        public abstract string GetFavouriteFood();
        public virtual void SayHello()
        {
            Console.WriteLine($"Hello from {Name}!");
        }
    }

    // Перечисление ClassificationAnimal
    public enum ClassificationAnimal
    {
        Herbivores,
        Carnivores,
        Omnivores
    }

    // Перечисление FavouriteFood
    public enum FavouriteFood
    {
        Meat,
        Plants,
        Everything
    }

    // Класс Cow
    [Comment("Represents a cow")]
    public class Cow : Animal
    {
        public ClassificationAnimal Classification { get; set; }

        public override string GetFavouriteFood()
        {
            return FavouriteFood.Plants.ToString();
        }
    }

    // Класс Lion
    [Comment("Represents a lion")]
    public class Lion : Animal
    {
        public ClassificationAnimal Classification { get; set; }

        public override string GetFavouriteFood()
        {
            return FavouriteFood.Meat.ToString();
        }
    }

    // Класс Pig
    [Comment("Represents a pig")]
    public class Pig : Animal
    {
        public ClassificationAnimal Classification { get; set; }

        public override string GetFavouriteFood()
        {
            return FavouriteFood.Everything.ToString();
        }
    }

}