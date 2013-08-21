namespace Chris.Common.Configuration.Elements
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using System.Collections;

    using Ninject;

    public class ActionElement : ConfigurationElement
    {
        [ConfigurationProperty("name" , IsKey = true, IsRequired = true, DefaultValue = "")]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }

        [ConfigurationProperty("actionType", IsKey = true, IsRequired = true, DefaultValue = "TakeNonNullValue")]
        public ActionTypeEnum ActionType
        {
            get
            {
                return (ActionTypeEnum)base["actionType"];
            }
            set
            {
                base["actionType"] = value;
            }
        }

        #region Enums

        public enum ActionTypeEnum
        {
            ReplaceSourceWithCompare,
            KeepSource,
            TakeNonNullValue,
            RemoveAttribute,
            AddArrayElement
        }

        #endregion

        public void Execute(FieldInfo field, object source, object compare, object arrayElement, bool missingInSource)
        {
            var kernel = new StandardKernel();

            GetActionHandler(kernel);

            var handler = kernel.Get<IActionHandler>();
            
            handler.Execute(field, source, compare, arrayElement, missingInSource);
        }

        public void GetActionHandler(StandardKernel kernel)
        {
            if (ActionType == ActionTypeEnum.ReplaceSourceWithCompare) kernel.Bind<IActionHandler>().To<ReplaceSourceWithCompareActionHandler>();
            else if (ActionType == ActionTypeEnum.KeepSource) kernel.Bind<IActionHandler>().To<KeepSourceActionHandler>();
            else if (ActionType == ActionTypeEnum.RemoveAttribute) kernel.Bind<IActionHandler>().To<RemoveAttributeActionHandler>();
            else if (ActionType == ActionTypeEnum.AddArrayElement) kernel.Bind<IActionHandler>().To<AddArrayElementActionHandler>();
            else kernel.Bind<IActionHandler>().To<TakeNonNullValueActionHandler>();
        }
    }

    public interface IActionHandler
    {
        void Execute(FieldInfo field, object source, object compare, object arrayElement, bool missingInSource);
    }

    public class ReplaceSourceWithCompareActionHandler : IActionHandler
    {
        public void Execute(FieldInfo field, object source, object compare, object arrayElement, bool missingInSource)
        {
            field.SetValue(source, field.GetValue(compare));
        }
    }

    public class KeepSourceActionHandler : IActionHandler
    {
        public void Execute(FieldInfo field, object source, object compare, object arrayElement, bool missingInSource)
        {
            field.SetValue(source, field.GetValue(source));
        }
    }

    public class TakeNonNullValueActionHandler : IActionHandler
    {
        public void Execute(FieldInfo field, object source, object compare, object arrayElement, bool missingInSource)
        {
            var specifiedField = source.GetType().GetFields().FirstOrDefault(x => x.Name == field.Name + "Specified");

            if (specifiedField != null)
            {
                if ((bool)specifiedField.GetValue(source) == false)
                {
                    field.SetValue(source, field.GetValue(compare));
                    specifiedField.SetValue(source, true);
                }
            }
            else
            {
                if (field.GetValue(source) == null)
                    field.SetValue(source, field.GetValue(compare));
            }
        }
    }

    public class RemoveAttributeActionHandler : IActionHandler
    {
        public void Execute(FieldInfo field, object source, object compare, object arrayElement, bool missingInSource)
        {
            field.SetValue(source, null);

            var specifiedField = source.GetType().GetFields().FirstOrDefault(x => x.Name == field.Name + "Specified");

            if (specifiedField != null)
            {
                specifiedField.SetValue(source, false);
            }
        }
       
    }

    public class AddArrayElementActionHandler : IActionHandler
    {
        public void Execute(FieldInfo field, object source, object compare, object arrayElement, bool missingInSource)
        {
            var incompleteList = compare;
            if (missingInSource)
                incompleteList = source;


            IList list = (IList)field.GetValue(incompleteList);
            list.Add(arrayElement);
        }

    }
}