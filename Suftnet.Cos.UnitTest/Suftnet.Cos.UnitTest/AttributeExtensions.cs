namespace Suftnet.Cos.UnitTest
{
   ﻿using System;
   using System.Reflection;

   public static  class AttributeExtensions
   {
        public static bool IsDecoratedWith<TAttribute>(this ICustomAttributeProvider attributeTarget) where TAttribute : Attribute
        {
            return attributeTarget.GetCustomAttributes(typeof(TAttribute), false).Length > 0;
        }

        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider attributeTarget) where TAttribute : Attribute
        {
            return (TAttribute)attributeTarget.GetCustomAttributes(typeof(TAttribute), false)[0];
        }

   }
}
