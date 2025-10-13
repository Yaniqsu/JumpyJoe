using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

/// <summary>
/// Pojedynczy wiersz w słowniku, głównie używany do wyświetlania danych ze słownika w inspektorze Unity
/// </summary>
/// <typeparam name="T1">Typ kluczu</typeparam>
/// <typeparam name="T2">Typ wartości</typeparam>
[System.Serializable]
public struct TableRow<T1,T2>
{
    public T1 key;
    public T2 value;

    public TableRow(T1 key, T2 value)
    {
        this.key = key;
        this.value = value;
    }

    public static Dictionary<T1, T2> ToDictionary(TableRow<T1, T2>[] array)
    {
        return array.ToDictionary(row => row.key, row => row.value);
    }
}