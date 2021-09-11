using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VladB.Utility {
    public static partial class Extensions {

        #region Act

        /// <summary>
        /// ��������� ��������� �������� �� ����� ���������� ������/�������/������ ������� IList
        /// </summary>
        /// <typeparam name="T">��� ��������� ������������ � IList</typeparam>
        /// <param name="iList">������, ������ ��� ����� ������ IList</param>
        /// <param name="action">�������, ����������� ������ ���� T � int(�������)</param>
        public static void Act<T>(this IList<T> iList, Action<T, int> action) {
            for(int i = 0; i < iList.Count; i++) {
                action.Invoke(iList[i], i);
            }
        }

        /// <summary>
        /// ��������� ��������� �������� �� ����� ���������� ������/�������/������ ������� IList, ���������� ���������� ForEach(x=>{blablabla})
        /// </summary>
        /// <typeparam name="T">��� ��������� ������������ � IList</typeparam>
        /// <param name="iList">������, ������ ��� ����� ������ IList</param>
        /// <param name="action">�������, ����������� ������ ���� T</param>
        public static void Act<T>(this IList<T> iList, Action<T> action) {
            for(int i = 0; i < iList.Count; i++) {
                action.Invoke(iList[i]);
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
        public static string GetString<T>(this IList<T> iList, string separator = " ") {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < iList.Count; i++) {
                sb.Append(iList[i] + ((i != iList.Count - 1) ? separator : ""));
            }
            return sb.ToString();
        }
        #endregion

        #region Random
        public static IList<T> GetSortedByRandom<T>(this IList<T> iList) {
            return iList.OrderBy((item) => UnityEngine.Random.Range(0f,1f)).ToList();
        }

        public static T GetRandom<T>(this IList<T> iList) {
            return (iList.Count > 0) ? iList[UnityEngine.Random.Range(0,iList.Count)] : default;
        }
        #endregion
    }
}
