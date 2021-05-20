using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerNews.CLI.Util
{
	public static class DictionaryUtil
	{
		public static Dictionary<TKey, TValue> KvpToDictionary<TKey, TValue>(IEnumerable<TKey> keys, IEnumerable<TValue> values)
		{
			TKey[] keyArr = keys.ToArray();
			TValue[] valueArr = values.ToArray();

			if (keyArr.Length != valueArr.Length) throw new Exception();

			var dict = new Dictionary<TKey, TValue>();

			for (int i = 0; i < keyArr.Length && i < valueArr.Length; i++)
			{
				dict.Add(keyArr[i], valueArr[i]);
			}

			return dict;
		}
	}
}
