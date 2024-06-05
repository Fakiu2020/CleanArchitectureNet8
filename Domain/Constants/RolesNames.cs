using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants
{
    public abstract class RolesNames(int id, string name) : Enumeration(id, name)
    {
        public static RolesNames Admin = new AdminType();
        public static RolesNames SuperAdmin = new SuperAdminType();
        public static RolesNames User = new UserType();

        private class AdminType : RolesNames
        {
            public AdminType() : base(2, "Admin")
            { }
        }

        private class SuperAdminType : RolesNames
        {
            public SuperAdminType() : base(1, "SuperAdmin")
            { }
        }

        private class UserType : RolesNames
        {
            public UserType() : base(3, "User")
            { }
        }

    }


    public abstract class Enumeration : IComparable
    {
        public string Name { get; private set; }

        public int Id { get; private set; }


        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public static T GetById<T>(int? id) where T : Enumeration
        {
            if (!id.HasValue) return null;
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>().FirstOrDefault(x => x.Id == id);
        }

        public static T GetById<T>(int id) where T : Enumeration
        {
            return GetById<T>((int?)id);
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);
    }

}
