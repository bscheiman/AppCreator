#region
using System;
using System.Linq.Expressions;

#endregion

namespace AppCreator.Extensions {
    public static class DataTemplateExtensions {
        public static void BindTo<TModel, TV>(this DataTemplate template, BindableProperty targetProperty, Expression<Func<TModel, TV>> func) {
            template.SetBinding(targetProperty, new Binding((func.Body as MemberExpression).Member.Name));
        }
    }
}