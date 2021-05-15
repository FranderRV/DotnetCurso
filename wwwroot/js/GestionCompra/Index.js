var listaProductos = [];
var producto = {};

$(document).ready(function () {
  let id = $("#IdCompra").val();
  if (id > 0) {
    AJAX(`/Compra/GetDetalles/${id}`, {}, "POST", function (response) {
      if (response.length > 0) {
        response.forEach((p) => {
          for (const property in p) {
            producto = {
              ...producto,
              [property.charAt(0).toUpperCase() + property.slice(1)]:
                p[property],
            };
          }
          producto = {
            ...producto,
            Nombre: producto.IdProductoNavigation.nombre,
            Iva: "0,13",
          };

          delete producto.IdDetalleCompra;
          delete producto.Total;
          delete producto.IdProductoNavigation;
          delete producto.IdCompraNavigation;

          listaProductos.push(producto);
        });
        ActualizarTabla();
      }
    });
  }

  /*
  setTimeout(() => {
    ActualizarTabla();
  }, 100); */
});

$("#JSON").click(function (e) {
  e.preventDefault();
  let action1 =
    $("#formEncabezado #IdCompra").val() != 0
      ? `ActualizarCompra`
      : "SetCompra";
  AJAX(
    `/Compra/${action1}`,
    $("#formEncabezado").serialize(),
    "POST",
    function (response) {
      console.log(response);

      if (response.state) {
        $("#formEncabezado #IdCompra").val(response.data);
        $("#formDetalle #IdCompra").val(response.data);

        listaProductos.forEach((p) => {
          delete p.Nombre;
          delete p.Indice;
          p.IdCompra = response.data;
        });
        let action2 =
          response.data != 0
            ? `ActualizarDetalles/${response.data}`
            : "JSONPrueba";
        console.log("listaProductos", listaProductos);
        AJAX(
          `/Compra/${action2}`,
          { DetalleCompra: listaProductos },
          "POST",
          function (response) {
            $(location).attr("href", "/Compra");
          }
        );
      }
    }
  );
});

$(document).on("submit", "#formDetalle", function (e) {
  e.preventDefault();

  ProcesarDetalle();

  listaProductos.push(producto);

  console.log(listaProductos);

  ActualizarTabla();
});

$("#actualizarDetalle").click(function (e) {
  e.preventDefault();
  ProcesarDetalle();

  listaProductos.forEach((p, i) => {
    if (p.IdProducto == producto.IdProducto) {
      listaProductos[i] = producto;
    }
  });

  ActualizarTabla();
});

function ProcesarDetalle() {
  $("#formDetalle")
    .serializeArray()
    .forEach((e) => {
      let indice = e.name.split(".")[1];

      if (indice != undefined) {
        producto = { ...producto, [indice]: e.value };
      }
    });

  producto = {
    ...producto,
    Nombre: $("#IdProducto option:selected").text(),
    Indice: $("#body").find("tr").length,
  };
}

function ActualizarTabla() {
  $("#body").empty();

  listaProductos.forEach((p, i) => {
    $("#body").append(
      `<tr>
      <td>${p.Nombre}</td> 
        <td>${p.Cantidad}</td>
        <td>${p.Precio}</td>
        <td>${p.Iva}</td>
        <td>${p.Cantidad * p.Precio}</td>
        <td><button type="button" onClick=EditarDetalle(${i}) class="btn btn-warning text-white">Editar</button></td>
        <td><button type="button" onClick=EliminarDetalle(${i}) class="btn btn-danger">Eliminar</button></td>
    </tr>`
    );
  });
}

function EditarDetalle(id) {
  producto = listaProductos[id];

  for (const property in producto) {
    $(`#formDetalle #${property}`).val(producto[property]);
  }

  $("#Productos").val(producto["IdProducto"]);
  $("#Productos").change();
}

function EliminarDetalle(id) {
  listaProductos = listaProductos.filter((e, i) => i != id);

  ActualizarTabla();
}
