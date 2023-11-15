# AlgLab7
# Лабораторная работа №7
«Рефлексия»

Цели работы:
Научиться работать с механизмом рефлексии языка C#.

# Задание№1
Создайте проект библиотеки классов со следующей диаграммой классов


В созданном проекте реализуйте класс пользовательского атрибута со свойством Comment. Примените атрибут с произвольным комментарием к каждому классу диаграммы классов.
Создайте приложение, которое будет подключать созданную библиотеку классов и средствами рефлексии генерировать файл xml-представления всей диаграммы классов библиотеки, в том числе с созданными пользовательскими атрибутами.

Class
```
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
```

Programm

```
using Lab37;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;


public class Program
{
    public static void Main(string[] args)
    {
        // Создаем экземпляр XmlWriterSettings
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;

        // Создаем экземпляр XmlWriter и указываем путь к файлу, в который будет записано xml-представление
        using (XmlWriter writer = XmlWriter.Create("class_diagram.xml", settings))
        {
            // Записываем заголовок xml
            writer.WriteStartDocument();
            writer.WriteStartElement("ClassDiagram");

            // Получаем сборку текущей сборки
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Получаем все типы в сборке
            Type[] types = assembly.GetTypes();

            // Перебираем каждый тип
            foreach (Type type in types)
            {
                // Получаем пользовательский атрибут класса (если есть)
                CommentAttribute classComment = (CommentAttribute)type.GetCustomAttribute(typeof(CommentAttribute));

                // Записываем информацию о классе
                writer.WriteStartElement("Class");
                writer.WriteAttributeString("Name", type.Name);

                // Записываем комментарий класса (если есть)
                if (classComment != null)
                {
                    writer.WriteElementString("Comment", classComment.Comment);
                }

                // Записываем информацию о свойствах класса
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    writer.WriteStartElement("Property");
                    writer.WriteAttributeString("Name", property.Name);
                    writer.WriteAttributeString("Type", property.PropertyType.Name);
                    writer.WriteEndElement();
                }

                // Записываем информацию о методах класса
                MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                foreach (MethodInfo method in methods)
                {
                    writer.WriteStartElement("Method");
                    writer.WriteAttributeString("Name", method.Name);
                    writer.WriteAttributeString("ReturnType", method.ReturnType.Name);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        // Выводим сообщение об успешном завершении программы
        Console.WriteLine("Xml-представление диаграммы классов сохранено в файл 'class_diagram.xml'");
    }
}
```
