using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharedHelpers
{
    public static class RecursiveValidator
    {
        /// <summary>
        /// Validate that not null and all annotations of <paramref name="obj"/> are correct.
        /// Most important, if missing property with annotation [Required] is found, an <see cref="InvalidOperationException"/> is thrown.
        /// </summary>
        /// <param name="obj">Object to validate</param>
        public static void Validate(object? obj)
        {
            var validationResultList = new List<ValidationResult>();
            var currentPath = string.Empty;
            ValidateInternal(obj, currentPath, validationResultList);
        }

        private static void ValidateInternal(object? obj, string currentPath, List<ValidationResult> validationResultList)
        {
            if (obj == null) { throw new ValidationException($"{nameof(obj)} must not be null"); }

            var beforeCount = validationResultList.Count;
            try
            {
                Validator.TryValidateObject(obj, new ValidationContext(obj), validationResultList, true);
            }
            catch (ValidationException ex)
            {
                validationResultList.Add(new ValidationResult(ex.Message));
            }

            if (currentPath.Length > 0)
            {
                foreach (var item in validationResultList.Skip(beforeCount))
                {
                    var e = item.ErrorMessage?.TrimEnd('.') ?? "Undefined error";
                    item.ErrorMessage = $"{e} in section \"{currentPath.TrimEnd('.')}\".";
                }
            }

            // get properties that can be validated
            List<PropertyInfo> properties = obj.GetType()
                .GetProperties()
                .Where(prop => prop.CanRead && prop.GetIndexParameters().Length == 0)
                .Where(prop => CanTypeBeValidated(prop.PropertyType))
                .ToList();

            // loop over each property
            foreach (PropertyInfo property in properties)
            {
                // get and check value
                var value = property.GetValue(obj);
                if (value == null)
                {
                    value = Activator.CreateInstance(property.PropertyType);
                    if (value == null) { throw new Exception("Could not create default instance of type " + property.PropertyType); }
                }

                // check whether its an enumerable - if not, put the value in a new enumerable
                if (!(value is IEnumerable<object> valueEnumerable))
                {
                    valueEnumerable = new object[] { value };
                }

                // validate values in enumerable
                foreach (var valueToValidate in valueEnumerable)
                {
                    ValidateInternal(valueToValidate, $"{currentPath}{property.Name}.", validationResultList);
                }
            }

            if (validationResultList.Count > 0)
            {
                var prefix = validationResultList.Count > 1 ? $"{validationResultList.Count} errors:" + Environment.NewLine : string.Empty;
                throw new ValidationException(prefix + string.Join(Environment.NewLine, validationResultList.Select(r => r.ErrorMessage)));
            }
        }

        /// <summary>
        /// Returns whether the given <paramref name="type"/> can be validated
        /// </summary>
        private static bool CanTypeBeValidated(Type type)
        {
            if (type == null)
            {
                return false;
            }

            if (type == typeof(string))
            {
                return false;
            }

            if (type.IsValueType)
            {
                return false;
            }

            if (type.IsArray && type.HasElementType)
            {
                var elementType = type.GetElementType();
                if (elementType == null) { return false; }
                return CanTypeBeValidated(elementType);
            }

            return true;
        }
    }
}
