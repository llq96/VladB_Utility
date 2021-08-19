using System;
using System.Collections.Generic;
using System.Linq;

namespace VladB.Utility {
    public static partial class Extensions {
        #region Act

        /// <summary>
        /// ��������� ��������� �������� �� ����� ���������� ������/�������/������ ������� IList
        /// </summary>
        /// <typeparam name="T">��� ��������� ������������ � IList</typeparam>
        /// <param name="list">������, ������ ��� ����� ������ IList</param>
        /// <param name="action">�������, ����������� ������ ���� T � int(�������)</param>
        public static void Act<T>(this IList<T> list, Action<T, int> action) {
            for(int i = 0; i < list.Count; i++) {
                action.Invoke(list[i], i);
            }
        }

        /// <summary>
        /// ��������� ��������� �������� �� ����� ���������� ������/�������/������ ������� IList, ���������� ���������� ForEach(x=>{blablabla})
        /// </summary>
        /// <typeparam name="T">��� ��������� ������������ � IList</typeparam>
        /// <param name="list">������, ������ ��� ����� ������ IList</param>
        /// <param name="action">�������, ����������� ������ ���� T</param>
        public static void Act<T>(this IList<T> list, Action<T> action) {
            for(int i = 0; i < list.Count; i++) {
                action.Invoke(list[i]);
            }
        }

        /// <summary>
        /// ��������� ��������� �������� �� ����� ���������� ������ IEnumerable
        /// </summary>
        /// <typeparam name="T">��� ��������� ������������ � IEnumerable</typeparam>
        /// <param name="list">����� IEnumerable</param>
        /// <param name="action">�������, ����������� ������ ���� T � int(�������)</param>
        public static void Act<T>(this IEnumerable<T> iEnumerable, Action<T, int> action) {
            iEnumerable.ToList().Act(action);
        }

        /// <summary>
        /// ��������� ��������� �������� �� ����� ���������� ������ IEnumerable
        /// </summary>
        /// <typeparam name="T">��� ��������� ������������ � IEnumerable</typeparam>
        /// <param name="list">����� IEnumerable</param>
        /// <param name="action">�������, ����������� ������ ���� T</param>
        public static void Act<T>(this IEnumerable<T> iEnumerable, Action<T> action) {
            iEnumerable.ToList().Act(action);
        }

        #endregion

        #region GetString
        //TODO Use StringBuilder
        public static string GetString<T>(this IList<T> list, string separator = " ") {
            string result = string.Empty;
            for(int i = 0; i < list.Count; i++) {
                result += list[i] + ((i != list.Count - 1) ? separator : "");
            }
            return result;
        }
        #endregion
    }

}
