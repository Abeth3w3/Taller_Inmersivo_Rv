using System;

public static class CoffeeProgressEvents
{
    public static event Action<ItemType> NuevoPedido;
    public static event Action TazaTomada;
    public static event Action RecipienteTomado;
    public static event Action LecheFriaObtenida;
    public static event Action LecheCalentandose;
    public static event Action LecheCalienteRecogida;
    public static event Action CafeNegroEnPreparacion;
    public static event Action CafeNegroRecogido;
    public static event Action CafeNegroEnMesa;
    public static event Action LecheVertida;
    public static event Action CafeConLecheRecogido;
    public static event Action PedidoEntregado;

    public static void DispararNuevoPedido(ItemType tipo) => NuevoPedido?.Invoke(tipo);
    public static void DispararTazaTomada() => TazaTomada?.Invoke();
    public static void DispararRecipienteTomado() => RecipienteTomado?.Invoke();
    public static void DispararLecheFriaObtenida() => LecheFriaObtenida?.Invoke();
    public static void DispararLecheCalentandose() => LecheCalentandose?.Invoke();
    public static void DispararLecheCalienteRecogida() => LecheCalienteRecogida?.Invoke();
    public static void DispararCafeNegroEnPreparacion() => CafeNegroEnPreparacion?.Invoke();
    public static void DispararCafeNegroRecogido() => CafeNegroRecogido?.Invoke();
    public static void DispararCafeNegroEnMesa() => CafeNegroEnMesa?.Invoke();
    public static void DispararLecheVertida() => LecheVertida?.Invoke();
    public static void DispararCafeConLecheRecogido() => CafeConLecheRecogido?.Invoke();
    public static void DispararPedidoEntregado() => PedidoEntregado?.Invoke();
}