using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria{get;}

        public List<Expression<Func<T, object>>> Includes{get;} 
          = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy{get;private set;}

        public Expression<Func<T, object>> OrderByDescending{get; private set;}

        public int Take {get; private set;}

        public int Skip{get; private set;}
        public bool IsPagingEnabled {get; private set;}

        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public void AddIncludes(Expression<Func<T,object>> includeExpression){
            Includes.Add(includeExpression);
        }

        public void AddOrderBy(Expression<Func<T,object>> orderByExpression){
          OrderBy=orderByExpression;
        }

        public void AddOrderByDescending(Expression<Func<T,object>> orderByDescendingExpression){
          OrderByDescending=orderByDescendingExpression;
        }

        public void ApplyPaging(int take, int skip){
          Skip=skip;
          Take=take;
          IsPagingEnabled=true;
        }
        
    }
}