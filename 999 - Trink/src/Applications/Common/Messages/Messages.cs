using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.Common.Messages
{
    public static class Messages
    {
        public static string AddRole ="Role added successfully";
        public static string UpdateRole = "Role updated successfully";
        public static string DeleteRole = "Role deleted successfully";
        public static string RoleNotFound = "Role not found";
        public static string RolesNotFound = "Roles not found";
        public static string ListRoles = "Roles listed successfully";
        public static string RoleAlreadyExists = "Role already exists";
        public static string RoleNameAlreadyExists = "Role name already exists";


        public static string RoleAddError = "An error occurred while adding the role";
        public static string RoleUpdateError = "An error occurred while updating the role";
        public static string RoleDeleteError = "An error occurred while deleting the role";

        public static string AddUser = "User added successfully";
        public static string UodateUser = "User updated successfully";
        public static string DeleteUser = "User deleted successfully";
        public static string UserNotFound = "User not found";
        public static string UsersNotFound = "Users not found";
        public static string ListUsers = "Users listed successfully";
        public static string UserAlreadyExists = "User already exists";
        public static string UserEmailAlreadyExists = "User email already exists";
        
    }
}