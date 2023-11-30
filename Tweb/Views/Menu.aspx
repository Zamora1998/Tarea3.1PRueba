<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="Tweb.Views.Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MENU Platillos Activos</title>
    <link rel="stylesheet" type="text/css" href="../CSS/style.css" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
   
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <div class="container">
            <a class="navbar-brand mr-auto" href="#">Administrador de Categorias</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item">
                        <a class="nav-link" href="Platillos.aspx">Platillos</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="Categorias.aspx">Categorias</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="Menu.aspx">Menu</a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <form id="form1" runat="server">
            <h3>Menu</h3>
            <table id="tablaPlatillos" class="table table-bordered  table-striped" >
                <thead>
                    <tr> 
                        <th class="text-center">ID</th>
                        <th class="text-center">Nombre</th>
                        <th class="text-center">Costo</th>
                        <th class="text-center">Categoría</th>
                        <th class="text-center">Estado</th> 
                    </tr>
                </thead>
                <tbody class="text-center"></tbody>
            </table> 
    </form>
    <script>  
        async function cargarPlatillosDesdeAPI() {
            try {
                const response = await fetch('https://tiusr30pl.cuc-carrera-ti.ac.cr/Rest/api/Platillos/ListarPlatillos');
                if (response.ok) {
                    const data = await response.json();
                    data.sort((a, b) => a.CategoriaNombre.localeCompare(b.CategoriaNombre));
                    const tabla = document.getElementById('tablaPlatillos').getElementsByTagName('tbody')[0];
                    data.forEach(platillo => {
                        if(platillo.EstadoDescripcion === 'Activo') {
                        const row = tabla.insertRow(); 
                        const idCell = row.insertCell();
                        const nombreCell = row.insertCell();
                        const costoCell = row.insertCell();
                        const categoriaCell = row.insertCell();
                        const estadoCell = row.insertCell();
                        idCell.textContent = platillo.PlatilloID;
                        nombreCell.textContent = platillo.Nombre;
                        costoCell.textContent = platillo.Costo.toFixed(2);
                        categoriaCell.textContent = platillo.CategoriaNombre;
                            estadoCell.textContent = platillo.EstadoDescripcion;
                        }
                    });
                } else {
                    mostrarError(errorMensaje, "Error al cargar datos desde la api.", false);
                }
            } catch (error) {
                mostrarError(errorMensaje, "Error inesperado.", true);
            }
        }
        cargarPlatillosDesdeAPI();
        
    </script>
    <style>
    #tablaPlatillos td {
        text-align: center;
    }
</style>
</body>
</html>
