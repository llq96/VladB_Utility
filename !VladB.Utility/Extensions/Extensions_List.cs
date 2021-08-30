using System;
using System.Collections.Generic;
using System.Linq;

namespace VladB.Utility {
    public static partial class Extensions {
        static Random random = new Random();

        #region Act

        /// <summary>
        /// Выполняет указанное действие со всеми элементами списка/массива/любого другого IList
        /// </summary>
        /// <typeparam name="T">Тип элементов содержащихся в IList</typeparam>
        /// <param name="iList">Список, массив или любой другой IList</param>
        /// <param name="action">Делегат, принимающий объект типа T и int(счётчик)</param>
        public static void Act<T>(this IList<T> iList, Action<T, int> action) {
            for(int i = 0; i < iList.Count; i++) {
                action.Invoke(iList[i], i);
            }
        }

        /// <summary>
        /// Выполняет указанное действие со всеми элементами списка/массива/любого другого IList, аналогичен дефолтному ForEach(x=>{blablabla})
        /// </summary>
        /// <typeparam name="T">Тип элементов содержащихся в IList</typeparam>
        /// <param name="iList">Список, массив или любой другой IList</param>
        /// <param name="action">Делегат, принимающий объект типа T</param>
        public static void Act<T>(this IList<T> iList, Action<T> action) {
            for(int i = 0; i < iList.Count; i++) {
                action.Invoke(iList[i]);
            }
        }

        /// <summary>
        /// Выполняет указанное действие со всеми элементами любого IEnumerable
        /// </summary>
        /// <typeparam name="T">Тип элементов содержащихся в IEnumerable</typeparam>
        /// <param name="list">Любой IEnumerable</param>
        /// <param name="action">Делегат, принимающий объект типа T и int(счётчик)</param>
        public static void Act<T>(this IEnumerable<T> iEnumerable, Action<T, int> action) {
            iEnumerable.ToList().Act(action);
        }

        /// <summary>
        /// Выполняет указанное действие со всеми элементами любого IEnumerable
        /// </summary>
        /// <typeparam name="T">Тип элементов содержащихся в IEnumerable</typeparam>
        /// <param name="list">Любой IEnumerable</param>
        /// <param name="action">Делегат, принимающий объект типа T</param>
        public static void Act<T>(this IEnumerable<T> iEnumerable, Action<T> action) {
            iEnumerable.ToList().Act(action);
        }

        #endregion

        #region GetString
        //TODO Use StringBuilder
        public static string GetString<T>(this IList<T> iList, string separator = " ") {
            string result = string.Empty;
            for(int i = 0; i < iList.Count; i++) {
                result += iList[i] + ((i != iList.Count - 1) ? separator : "");
            }
            return result;
        }
        #endregion

        #region Random
        public static IList<T> GetSortedByRandom<T>(this IList<T> iList) {
            return iList.OrderBy((item) => random.Next()).ToList();
        }

        public static T GetRandom<T>(this IList<T> iList) {
            return (iList.Count > 0) ? iList[random.Next()] : default;
        }
        #endregion
    }

}
