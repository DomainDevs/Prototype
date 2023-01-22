using System.Reflection;

namespace Prototype.Infrastructure.Common.Extensions
{
    public static class TypeExtensions
    {
        /*
         * OBTENER LISTA DE CONSTANTES DE UNA CLASE:

        Si desea obtener los valores de todas las constantes de un 
        tipo específico, del tipo de destino, aquí hay un método 
        de extensión (ampliando algunas de las respuestas en esta página):
        
        Entonces de esto:
        static class MyFruitKeys
        {
            public const string Apple = "apple";
            public const string Plum = "plum";
            public const string Peach = "peach";
            public const int WillNotBeIncluded = -1;
        }

        Puedes obtener los stringvalores constantes así:
        List<string> result = typeof(MyFruitKeys).GetAllPublicConstantValues<string>();
        //result[0] == "apple"
        //result[1] == "plum"
        //result[2] == "peach"
         */
        public static List<T> GetAllPublicConstantValues<T>(this Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
                .Select(x => x.GetRawConstantValue())
                .Where(x => x is not null)
                .Cast<T>()
                .ToList();
        }

        public static List<string> GetNestedClassesStaticStringValues(this Type type)
        {
            var values = new List<string>();
            foreach (var prop in type.GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                object? propertyValue = prop.GetValue(null);
                if (propertyValue?.ToString() is string propertyString)
                {
                    values.Add(propertyString);
                }
            }

            return values;
        }
    }
}
