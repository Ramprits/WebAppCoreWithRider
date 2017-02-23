using System;
using System.Linq.Expressions;

namespace WebAppCore.Infrastructure
{
    public static class Utilities
    {
        public static Expression<Func<TEntity, bool>> BuildLambdaForFindByKey<TEntity>(int id)
        {

            var item = Expression.Parameter(type: typeof(TEntity), name: "entity");


            var prop = Expression.Property(expression: item, propertyName: typeof(TEntity).Name + "Id");


            var value = Expression.Constant(value: id);


            var equal = Expression.Equal(left: prop, right: value);


            var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, item);

            return lambda;
        }
    }
}