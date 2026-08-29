using UnityEngine;
using TMPro;

public class TaskChecklistUI : MonoBehaviour
{
    public TextMeshProUGUI texto;

    private readonly string[] pasosCafeNegro = new string[]
    {
        "Toma una taza vacia",
        "Coloca la taza en la cafetera",
        "Recoge el cafe negro de la cafetera",
        "Entrega el cafe al cliente"
    };

    private readonly string[] pasosCafeConLeche = new string[]
    {
        "Toma una taza vacia",
        "Coloca la taza en la cafetera",
        "Toma un recipiente vacio",
        "Llena el recipiente de leche en la nevera",
        "Coloca el recipiente en la estufa",
        "Recoge la leche caliente",
        "Recoge el cafe negro de la cafetera",
        "Deja el cafe negro en la mesa",
        "Vierte la leche caliente sobre el cafe",
        "Recoge el cafe con leche",
        "Entrega el cafe al cliente"
    };

    private string[] pasosActuales;
    private bool[] completado;
    private ItemType tipoActual = ItemType.None;

    void OnEnable()
    {
        CoffeeProgressEvents.NuevoPedido += OnNuevoPedido;
        CoffeeProgressEvents.TazaTomada += OnTazaTomada;
        CoffeeProgressEvents.CafeNegroEnPreparacion += OnCafeNegroEnPreparacion;
        CoffeeProgressEvents.RecipienteTomado += OnRecipienteTomado;
        CoffeeProgressEvents.LecheFriaObtenida += OnLecheFriaObtenida;
        CoffeeProgressEvents.LecheCalentandose += OnLecheCalentandose;
        CoffeeProgressEvents.LecheCalienteRecogida += OnLecheCalienteRecogida;
        CoffeeProgressEvents.CafeNegroRecogido += OnCafeNegroRecogido;
        CoffeeProgressEvents.CafeNegroEnMesa += OnCafeNegroEnMesa;
        CoffeeProgressEvents.LecheVertida += OnLecheVertida;
        CoffeeProgressEvents.CafeConLecheRecogido += OnCafeConLecheRecogido;
        CoffeeProgressEvents.PedidoEntregado += OnPedidoEntregado;

        MostrarSinPedido();
    }

    void OnDisable()
    {
        CoffeeProgressEvents.NuevoPedido -= OnNuevoPedido;
        CoffeeProgressEvents.TazaTomada -= OnTazaTomada;
        CoffeeProgressEvents.CafeNegroEnPreparacion -= OnCafeNegroEnPreparacion;
        CoffeeProgressEvents.RecipienteTomado -= OnRecipienteTomado;
        CoffeeProgressEvents.LecheFriaObtenida -= OnLecheFriaObtenida;
        CoffeeProgressEvents.LecheCalentandose -= OnLecheCalentandose;
        CoffeeProgressEvents.LecheCalienteRecogida -= OnLecheCalienteRecogida;
        CoffeeProgressEvents.CafeNegroRecogido -= OnCafeNegroRecogido;
        CoffeeProgressEvents.CafeNegroEnMesa -= OnCafeNegroEnMesa;
        CoffeeProgressEvents.LecheVertida -= OnLecheVertida;
        CoffeeProgressEvents.CafeConLecheRecogido -= OnCafeConLecheRecogido;
        CoffeeProgressEvents.PedidoEntregado -= OnPedidoEntregado;
    }

    private void OnNuevoPedido(ItemType tipo)
    {
        tipoActual = tipo;
        pasosActuales = tipo == ItemType.MilkCoffee ? pasosCafeConLeche : pasosCafeNegro;
        completado = new bool[pasosActuales.Length];
        Actualizar();
    }

    private void OnTazaTomada() => Marcar(0);
    private void OnCafeNegroEnPreparacion() => Marcar(1);

    private void OnRecipienteTomado()
    {
        if (tipoActual == ItemType.MilkCoffee) Marcar(2);
    }

    private void OnLecheFriaObtenida()
    {
        if (tipoActual == ItemType.MilkCoffee) Marcar(3);
    }

    private void OnLecheCalentandose()
    {
        if (tipoActual == ItemType.MilkCoffee) Marcar(4);
    }

    private void OnLecheCalienteRecogida()
    {
        if (tipoActual == ItemType.MilkCoffee) Marcar(5);
    }

    private void OnCafeNegroRecogido()
    {
        Marcar(tipoActual == ItemType.MilkCoffee ? 6 : 2);
    }

    private void OnCafeNegroEnMesa()
    {
        if (tipoActual == ItemType.MilkCoffee) Marcar(7);
    }

    private void OnLecheVertida()
    {
        if (tipoActual == ItemType.MilkCoffee) Marcar(8);
    }

    private void OnCafeConLecheRecogido()
    {
        if (tipoActual == ItemType.MilkCoffee) Marcar(9);
    }

    private void OnPedidoEntregado()
    {
        MostrarSinPedido();
    }

    private void Marcar(int indice)
    {
        if (completado == null || indice < 0 || indice >= completado.Length) return;

        completado[indice] = true;
        Actualizar();
    }

    private void MostrarSinPedido()
    {
        tipoActual = ItemType.None;
        pasosActuales = null;
        completado = null;

        if (texto != null)
        {
            texto.text = "Esperando el pedido del proximo cliente...";
        }
    }

    private void Actualizar()
    {
        if (texto == null || pasosActuales == null) return;

        string resultado = "";
        for (int i = 0; i < pasosActuales.Length; i++)
        {
            string marca = completado[i] ? "[x] " : "[ ] ";
            resultado += marca + pasosActuales[i] + "\n";
        }

        texto.text = resultado;
    }
}