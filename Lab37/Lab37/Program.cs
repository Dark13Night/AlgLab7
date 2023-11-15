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