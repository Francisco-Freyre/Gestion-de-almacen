
using Punto_de_ventas.Connection;
using Punto_de_ventas.models;
using Punto_de_ventas.modelsclass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_ventas
{
    public partial class Form1 : Form
    {
        private string accion = "insert", deudaActual, pago, dia, role, usuario, tipo = "", usuariocorte = "", pagocon = "", sucambio = "", categoriapedido = "";
        private int paginas = 0, tamañoPagina = 10000, maxReg, contadorPagina, idCliente = 0, IdRegistro = 0, idUsuario,
            IdProducto = 0, caja, idDpto = 0, idCat = 0, numPag = 1, idusuarioeliminar = -1;
        private string fecha = DateTime.Now.ToString("dd/MMM/yyy");
        private string hora = DateTime.Now.ToString("hh:mm:ss");
        private List<Label> labels = new List<Label>();
        private GroupBox groupBox;
        private DateTimePicker dateTimePicker;
        private object[] textBoxObject, labelsObject;

        public Form1(List<usuarios> listUsuario, List<Cajas> listCaja)
        {

            InitializeComponent();
            new Conexion();
            textBox_BuscarProductos.Focus();
            if (null != listUsuario)
            {
                role = listUsuario[0].Rol;
                idUsuario = listUsuario[0].IdUsuario;
                usuario = listUsuario[0].Usuario;
                if ("Admin" == role)
                {
                    //label_Usuario.Text = listUsuario[0].Usuario;
                   // label_Caja.Text = "0";
                    ClassModels.ejemplo_listUsuario = listUsuario;
                    caja = 0;
                    //panel1.Show();
                    panel2.Show();
                    panel3.Hide();
                    groupBox43.Show();
                }
                else
                {
                    //label_Usuario.Text = listUsuario[0].Usuario;
                    //label_Caja.Text = Convert.ToString(listCaja[0].Caja);
                    ClassModels.ejemplo_listCaja = listCaja;
                    ClassModels.ejemplo_listUsuario = listUsuario;
                    caja = listCaja[0].Caja;
                    //panel1.Hide();
                    panel2.Hide();
                    panel3.Hide();
                    groupBox43.Hide();
                    label123.Hide();
                    //botones ocultos por peticion del cliente
                    button_BuscarProducto.Hide();
                    //button_CancelarVenta.Hide();
                }
            }

            timer1.Start();


            if (role != "Admin")
            {
                radioButton_PagosDeudas.Checked = false;
                //button_Clientes.Enabled = true;
                //button_Dpto.Enabled = false;
                //button_Compras.Enabled = false;
                ///button_Config.Enabled = false;
                //button_Corte.Enabled = false;
                //buttonVentas_Dia.Enabled = false;
            }

            radioButton_IngresarCliente.Checked = true;
            radioButton_IngresarCliente.ForeColor = Color.DarkCyan;
            //ClassModels.cliente.getClienteReporte(dataGridView_ClienteReporte, idCliente);
            //ClassModels.cliente.buscarCliente(dataGridView_Cliente, "", 1, tamañoPagina);

            ClassModels.venta.start(caja, idUsuario);
            //labels.Add(label_Deuda);
            //labels.Add(label_ReciboDeuda);
            //labels.Add(label_ReciboDeudaTotal);
           // labels.Add(label_ReciboNombre);
            //labels.Add(label_ReciboDeudaAnterior);
            //labels.Add(label_ReciboUltimoPago);
            //labels.Add(label_ReciboFecha);
            //labels.Add(label47);
            labels.Add(label_MensajeCliente);
            //labels.Add(label_SuCambio);
            //button_Ventas.Enabled = false;
            restablecerVentas();

            object[] textBoxObject = {
                textBoxUser_Nombre, textBoxUser_Apellido, textBoxUser_Telefono, textBoxUser_Direccion,
                textBoxUser_Correo, textBoxUser_Contraseña, textBoxUser_Usuario, textBoxUser_Roles
            };
            object[] labelsObject = {
                labelUser_Nombre, labelUser_Apellido, labelUser_Telefono, labelUser_Direccion,
                labelUser_Correo, labelUser_Contraseña, labelUser_Usuario, labelUser_Roles
            };

            this.textBoxObject = textBoxObject;
            this.labelsObject = labelsObject;


            labelCajas = new List<Label>();
            labelCajas.Add(labelCaja_Ingresos);
            labelCajas.Add(labelCaja_Reirar);
            labelCajas.Add(label_CajaIngresos);
            labelCajas.Add(label69);

            textBoxCajas = new List<TextBox>();
            textBoxCajas.Add(textBox_Retirar);
            textBoxCajas.Add(textBoxCaja_IngresoInicial);

            object[] objectCajas = { dataGridView_CajasIngresos, dateTimePicker_Cajas, idUsuario, dataGridViewCajas_Registros };
            // this.objectCajas = objectCajas;
            objectCaja = new Caja(objectCajas, labelCajas, textBoxCajas);


            labelProductos = new List<Label>();
            labelProductos.Add(label52);
            labelProductos.Add(labelInventario_Paginas);
            labelProductos.Add(label71);
            textBoxProductos = new List<TextBox>();
            textBoxProductos.Add(textBoxInventario_Existencia);

            object[] objectInventario = { dataGridView_Inventario,
                checkBoxInventario_Agotados,
                tabControlInv,
                dataGridViewInv_Ventas,
                dateTimePickerInv_Ventas,
                checkBoxInv_MaxVentas,
                dateTimePickerInv_Ventas2};
            inventario = new Inventario(objectInventario, labelProductos, textBoxProductos);
            ClassModels.invetario = inventario;

            comboBoxCorte_Cajero.DataSource = ClassModels.usuario.getUsuarios();
            comboBoxCorte_Cajero.ValueMember = "IdUsuario";
            comboBoxCorte_Cajero.DisplayMember = "Usuario";
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (paginas)
            {
                case 0:
                    //ClassModels.venta.ingresosCajas(label_iniVentas, label_ingresosVenta, label_ingresosTotalesVentas
                       // , caja, idUsuario);
                    break;
                case 6:
                    objectCaja.getCajas();
                    break;
                case 7:
                    inventario.getProducto("", 1, tamañoPagina, labelCostoInventarioCompleto, labelGananciaAprox);
                    break;
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ClassModels.imprimir.printDocument(e, groupBox, caja, idUsuario, tipo, dateTimePicker, usuariocorte, pagocon, sucambio);
        }
        //Codigo de clientes ######################################################
        #region

        private void radioButton_IngresarCliente_CheckedChanged(object sender, EventArgs e)
        {
            radioButton_IngresarCliente.ForeColor = Color.DarkCyan;
            radioButton_PagosDeudas.ForeColor = Color.Black;
            textBox_Id.ReadOnly = false;
            textBox_Nombre.ReadOnly = false;
            textBox_Apellido.ReadOnly = false;
            textBox_Direccion.ReadOnly = false;
            textBox_Telefono.ReadOnly = false;
            textBox_PagoscCliente.ReadOnly = true;
            label_PagoCliente.Text = "Pagos de deudas ";
            label_PagoCliente.ForeColor = Color.DarkCyan;
        }

        private void radioButton_PagosDeudas_CheckedChanged(object sender, EventArgs e)
        {

            radioButton_PagosDeudas.ForeColor = Color.DarkCyan;
            radioButton_IngresarCliente.ForeColor = Color.Black;
            textBox_Id.ReadOnly = true;
            textBox_Nombre.ReadOnly = true;
            textBox_Apellido.ReadOnly = true;
            textBox_Direccion.ReadOnly = true;
            textBox_Telefono.ReadOnly = true;
            textBox_PagoscCliente.ReadOnly = false;
        }

        private void textBox_Id_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Id.Text == "") { label_Id.ForeColor = Color.LightSlateGray; }
            else
            {
                label_Id.Text = "ID";
                label_Id.ForeColor = Color.Green;
            }
        }

        private void textBox_Id_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.numberKeyPress(e);
        }

        private void textBox_Nombre_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Nombre.Text == "") { label_Nombre.ForeColor = Color.LightSlateGray; }
            else
            {
                label_Nombre.Text = "Nombre completo";
                label_Nombre.ForeColor = Color.Green;
            }
        }

        private void textBox_Nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.textKeyPress(e);
        }

        private void textBox_Apellido_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Apellido.Text == "") { label_Apellido.ForeColor = Color.LightSlateGray; }
            else
            {
                label_Apellido.Text = "Apellido";
                label_Apellido.ForeColor = Color.Green;
            }
        }

        private void textBox_Apellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.textKeyPress(e);
        }

        private void textBox_Direccion_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Direccion.Text == "") { label_Direccion.ForeColor = Color.LightSlateGray; }
            else
            {
                label_Direccion.Text = "Telefono";
                label_Direccion.ForeColor = Color.Green;
            }
        }

        private void textBox_Direccion_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox_Telefono_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Telefono.Text == "") { label_Telefono.ForeColor = Color.LightSlateGray; }
            else
            {
                label_Telefono.Text = "Limite de credito ('0' es Sin Limite )";
                label_Telefono.ForeColor = Color.Green;
            }
        }

        private void textBox_Telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.numberKeyPress(e);
        }

        private void textBox_PagoscCliente_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView_ClienteReporte.CurrentRow == null)
            {
                label_PagoCliente.Text = "Seleccione el cliente";
                label_PagoCliente.ForeColor = Color.Red;
                textBox_PagoscCliente.Text = "";
            }
            else
            {
                if (textBox_PagoscCliente.Text != "")
                {
                    label_PagoCliente.Text = "Pagos de deudas";
                    label_PagoCliente.ForeColor = Color.LightSlateGray;
                    String deuda1;
                    Decimal deuda2, deuda3, deudaTotal;
                    deuda1 = Convert.ToString(dataGridView_ClienteReporte.CurrentRow.Cells[3].Value);
                    deuda1 = deuda1.Replace("$", "");
                    deuda2 = Convert.ToDecimal(deuda1);

                    deuda3 = Convert.ToDecimal(textBox_PagoscCliente.Text);

                    deudaTotal = deuda2 - deuda3;
                    deudaActual = String.Format("{0: #,###,###,##0.00####}", deudaTotal);
                    deudaActual = deudaActual.Replace(" ", "");
                    pago = String.Format("{0: #,###,###,##0.00####}", textBox_PagoscCliente.Text);

                }

            }

        }

        private void textBox_PagoscCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.numberDecimalKeyPress(textBox_PagoscCliente, e);
        }

        private void button_GuardarCliente_Click(object sender, EventArgs e)
        {
            if (radioButton_IngresarCliente.Checked)
            {
                guardarCliente();
            }
            else
            {
                guardarPago();
            }
        }

        private void guardarPago()
        {
            if (textBox_PagoscCliente.Text == "")
            {
                label_PagoCliente.Text = "Ingrese pago";
                label_PagoCliente.ForeColor = Color.Red;
                textBox_PagoscCliente.Focus();
            }
            else
            {
                ClassModels.cliente.actualizaRep(deudaActual, pago, idCliente, usuario);
                restablecer();
            }
        }

        private void button_Cancelar_Click(object sender, EventArgs e)
        {
            restablecer();
        }

        private void dataGridView_Cliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_Cliente.Rows.Count != 0) { dataGridViewCliente(); }
            idCliente = Convert.ToInt16(dataGridView_Cliente.CurrentRow.Cells[0].Value);
            ClassModels.cliente.buscarProductoCliente(dataGridViewClientes_Productos, idCliente, numPag, tamañoPagina);
        }

        private void dataGridView_Cliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView_Cliente.Rows.Count != 0) { dataGridViewCliente(); }
        }

        private void dataGridViewCliente()
        {
            accion = "update";
            idCliente = Convert.ToInt16(dataGridView_Cliente.CurrentRow.Cells[0].Value);
            textBox_Id.Text = Convert.ToString(dataGridView_Cliente.CurrentRow.Cells[1].Value);
            textBox_Nombre.Text = Convert.ToString(dataGridView_Cliente.CurrentRow.Cells[2].Value);
            textBox_Apellido.Text = Convert.ToString(dataGridView_Cliente.CurrentRow.Cells[3].Value);
            textBox_Direccion.Text = Convert.ToString(dataGridView_Cliente.CurrentRow.Cells[4].Value);
            textBox_Telefono.Text = Convert.ToString(dataGridView_Cliente.CurrentRow.Cells[5].Value);
            ClassModels.cliente.getClienteReporte(dataGridView_ClienteReporte, idCliente);
        }
        private void button_EliminarClientes_Click(object sender, EventArgs e)
        {
            if (idCliente > 0)
            {
                if (MessageBox.Show("¿Estas seguro de eliminar este cliente?", "Eliminar cliente",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ClassModels.cliente.borrarCliente(idCliente, IdRegistro);
                    restablecer();
                }
            }
        }


        private void textBox_BuscarCliente_TextChanged(object sender, EventArgs e)
        {
            ClassModels.cliente.buscarCliente(dataGridView_Cliente, textBox_BuscarCliente.Text, 1, tamañoPagina);
        }

        private void button_ImprCliente_Click(object sender, EventArgs e)
        {
            groupBox = groupBox_ReciboCliente;
            printDocument1.Print();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Estas seguro de salir?", "Cerrar sesion",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ClassModels.login.Visible = true;
                int idUsuario = ClassModels.ejemplo_listUsuario[0].IdUsuario;
                string nombre = ClassModels.ejemplo_listUsuario[0].Nombre;
                string apellido = ClassModels.ejemplo_listUsuario[0].Apellido;
                string user = ClassModels.ejemplo_listUsuario[0].Usuario;
                if (role == "Admin")
                {
                    
                }
                else
                {
                    int idCaja = ClassModels.ejemplo_listCaja[0].IdCaja;
                    int cajas = ClassModels.ejemplo_listCaja[0].Caja;
                    ClassModels.Caja.actulizarCaja(idCaja, true);
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion

        //Codigo de departamento ###################################################
        #region

        private void button_Dpto_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
            restablecerDptoCat();
            //button_Clientes.Enabled = true;
            //button_Ventas.Enabled = true;
            //button_Productos.Enabled = true;
            //button_Compras.Enabled = true;
            //button_Dpto.Enabled = false;
            //button_Config.Enabled = true;
            radioButton_Dpto.ForeColor = Color.DarkCyan;
            textBox_Cat.ReadOnly = true;
        }



        private void radioButton_Dpto_CheckedChanged(object sender, EventArgs e)
        {
            radioButton_Cat.ForeColor = Color.LightSlateGray;
            radioButton_Dpto.ForeColor = Color.DarkCyan;
            textBox_Dpto.ReadOnly = false;
            textBox_Dpto.Text = "";
            textBox_Cat.ReadOnly = true;
            label_Cat.Text = "Categoria";
            label_Cat.ForeColor = Color.LightSlateGray;
        }
        private void radioButton_Cat_CheckedChanged(object sender, EventArgs e)
        {
            radioButton_Dpto.ForeColor = Color.LightSlateGray;
            radioButton_Cat.ForeColor = Color.DarkCyan;
            textBox_Dpto.ReadOnly = true;
            textBox_Cat.Text = "";
            textBox_Cat.ReadOnly = false;
            label_Dpto.Text = "Departamento";
            label_Dpto.ForeColor = Color.LightSlateGray;
        }

        private void textBox_Dpto_TextChanged(object sender, EventArgs e)
        {
            if (label_Dpto.Text == "")
            {
                label_Dpto.ForeColor = Color.LightSlateGray;
            }
            else
            {
                label_Dpto.Text = "Departamento";
                label_Dpto.ForeColor = Color.DarkCyan;
            }
        }

        private void textBox_Dpto_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.textKeyPress(e);
        }

        private void textBox_Cat_TextChanged(object sender, EventArgs e)
        {
            if (label_Cat.Text == "")
            {
                label_Cat.ForeColor = Color.LightSlateGray;
            }
            else
            {
                label_Cat.Text = "Categoria";
                label_Cat.ForeColor = Color.DarkCyan;
            }
        }

        private void textBox_Cat_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.textKeyPress(e);
        }

        private void button_CuardarDpto_Click(object sender, EventArgs e)
        {
            GuardarDptoCat();
        }

        private void GuardarDptoCat()
        {
            bool valor = true;
            if (radioButton_Dpto.Checked)
            {
                if (textBox_Dpto.Text == "")
                {
                    label_Dpto.Text = "Ingrese el departamento";
                    label_Dpto.ForeColor = Color.Red;
                    textBox_Dpto.Focus();
                }
                else
                {
                    switch (accion)
                    {
                        case "insert":
                            valor = ClassModels.dptocat.insertarDptoCat(textBox_Dpto.Text, 0, "dpto");
                            break;
                        case "update":
                            valor = ClassModels.dptocat.actualizarDptoCat(textBox_Dpto.Text, idDpto, 0, "dpto");
                            break;
                    }
                    if (valor == false)
                    {
                        label_Dpto.Text = "Ya existe este departamento";
                        label_Dpto.ForeColor = Color.Red;
                    }
                    else
                    {
                        restablecerDptoCat();
                    }

                }
            }

            if (radioButton_Cat.Checked)
            {
                if (textBox_Cat.Text == "")
                {
                    label_Cat.Text = "Ingrese categoria";
                    label_Cat.ForeColor = Color.Red;
                    textBox_Cat.Focus();
                }
                else
                {
                    if (idDpto != 0)
                    {
                        switch (accion)
                        {
                            case "insert":
                                valor = ClassModels.dptocat.insertarDptoCat(textBox_Cat.Text, idDpto, "cat");
                                break;
                            case "update":
                                valor = ClassModels.dptocat.actualizarDptoCat(textBox_Cat.Text, idDpto, idCat, "cat");
                                break;
                        }
                        if (valor == false)
                        {
                            label_Cat.Text = "La categoria ya existe";
                            label_Cat.ForeColor = Color.Red;
                        }
                        else
                        {
                            restablecerDptoCat();
                        }

                    }
                    else
                    {
                        label_Dpto.Text = "Seleccione un departamento";
                        label_Dpto.ForeColor = Color.Red;
                    }
                }
            }
        }

        private void restablecerDptoCat()
        {
            idCat = 0;
            idDpto = 0;
            accion = "insert";
            textBox_Dpto.Text = "";
            textBox_Cat.Text = "";
            label_Dpto.Text = "Departamento";
            label_Dpto.ForeColor = Color.LightSlateGray;
            label_Cat.Text = "Categoria";
            label_Cat.ForeColor = Color.LightSlateGray;
            textBox_Dpto.Focus();
            radioButton_Dpto.Checked = true;
            radioButton_Dpto.ForeColor = Color.DarkCyan;
            textBox_Cat.ReadOnly = true;
            ClassModels.dptocat.buscarDpto(dataGridView_Dpto, "", 0, 1);
            ClassModels.dptocat.buscarDpto(dataGridView_Cat, "", 0, 2);

        }


        private void dataGridView_Dpto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_Dpto.Rows.Count != 0)
                dataGridViewDpto();
        }

        private void dataGridView_Dpto_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView_Dpto.Rows.Count != 0)
                dataGridViewDpto();
        }

        private void dataGridViewDpto()
        {
            if (radioButton_Dpto.Checked)
                accion = "update";
            idDpto = Convert.ToInt16(dataGridView_Dpto.CurrentRow.Cells[0].Value);
            textBox_Dpto.Text = Convert.ToString(dataGridView_Dpto.CurrentRow.Cells[1].Value);
            if (radioButton_Cat.Checked)
            {
                accion = "insert";
                label_Cat.Text = "Categoria";
                label_Cat.ForeColor = Color.LightSlateGray;
            }
            ClassModels.dptocat.buscarDpto(dataGridView_Cat, "", idDpto, 2);
        }

        private void dataGridView_Cat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_Cat.Rows.Count != 0)
                dataGridViewCat();
        }

        private void dataGridView_Cat_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView_Cat.Rows.Count != 0)
                dataGridViewCat();
        }

        private void dataGridViewCat()
        {
            idCat = Convert.ToInt16(dataGridView_Cat.CurrentRow.Cells[0].Value);
            textBox_Cat.Text = Convert.ToString(dataGridView_Cat.CurrentRow.Cells[1].Value);
            accion = "update";
        }

        private void button_EliminarDpto_Click(object sender, EventArgs e)
        {
            if (radioButton_Dpto.Checked)
            {
                if (idDpto > 0)
                {
                    if (MessageBox.Show("¿Estas seguro de eliminar este departamento?", "Eliminar departamento",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ClassModels.dptocat.eliminarDptoCat(idDpto, idCat, "dpto");
                        restablecerDptoCat();
                    }
                }
            }
            else
            {
                if (idCat > 0)
                {
                    if (MessageBox.Show("¿Estas seguro de eliminar esta categoria?", "Eliminar categoria",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ClassModels.dptocat.eliminarDptoCat(idDpto, idCat, "Cat");
                        restablecerDptoCat();
                    }
                }
            }
        }

        private void button_DptoCancelar_Click(object sender, EventArgs e)
        {
            restablecerDptoCat();
        }

        private void textBox_BuscarDpto_TextChanged(object sender, EventArgs e)
        {
            ClassModels.dptocat.buscarDpto(dataGridView_Dpto, textBox_BuscarDpto.Text, 0, 1);
        }

        #endregion

        //Codigo viejo que no sirve pero igualmente no hay que mover de momento ###################################################
        #region

        private void textBox_CodigoBarras_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox_DescripCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox_PrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox_NuevoProductoDpto_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.textKeyPress(e);
        }

        #endregion

        // Codigo de inventario ###############################################
        #region
        private void button_Productos_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            restablecerInvProd();
            comboBoxProvedor2.DataSource = ClassModels.Products.getCategoria();
            comboBoxProvedor2.DisplayMember = "Categoria";
            if (role != "Admin")
            {
                //button_Clientes.Enabled = true;
                //button_Ventas.Enabled = true;
                //button_Productos.Enabled = false;
                //button_Compras.Enabled = false;
                //button_Dpto.Enabled = false;
                //button_Config.Enabled = false;
                //button_Corte.Enabled = false;
            }
            else
            {
                //button_Productos.Enabled = false;
            }
            ClassModels.merma.mostrarGrid("", dataGridViewMerma);
        }

        private void textBox_CoprasProductos_TextChanged(object sender, EventArgs e)
        {
            ClassModels.Products.buscarProductoInventario(textBox_CoprasProductos.Text, label_paraDescripcion2,
                label_paraExistencia2);
        }

        private void textBox_CoprasProductos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ClassModels.Products.buscarProductoInventario(textBox_CoprasProductos.Text, label_paraDescripcion2,
                label_paraExistencia2);
                textBox_PrecioVentaPDT.Focus();
            }
        }

        private void textBox_PrecioVentaPDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ClassModels.Products.actualizarProductoInventario(textBox_CoprasProductos.Text, textBox_PrecioVentaPDT.Text,
                label_paraExistencia2.Text, label_DepartamentoPDT, usuario);
                restablecerInvProd();
                textBox_CoprasProductos.Focus();
            }
        }

        private void button_GuardarPDT_Click(object sender, EventArgs e)
        {
            ClassModels.Products.actualizarProductoInventario(textBox_CoprasProductos.Text, textBox_PrecioVentaPDT.Text,
                label_paraExistencia2.Text, label_DepartamentoPDT, usuario);
            restablecerInvProd();
        }

        private void restablecerInvProd()
        {
            textBox_CoprasProductos.Text = "";
            textBox_PrecioVentaPDT.Text = "";
            label_paraDescripcion2.Text = "";
            label_paraExistencia2.Text = "";
            label_DepartamentoPDT.Text = "No. de productos";
            label_DepartamentoPDT.ForeColor = Color.Black;
        }

        #endregion

        //Codigo reporte de inventarios #######################################
        #region


        private void button_ReporteMoviminetos_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 8;

            //button_Clientes.Enabled = true;
            //button_Ventas.Enabled = true;
           // button_Productos.Enabled = true;
            //button_Compras.Enabled = true;
            //button_Dpto.Enabled = true;
            //button_Config.Enabled = true;
            ClassModels.Reportes.buscarReportes(dateTimePicker_DiaHistorial, dataGridView_HistorialInventario);
        }

        private void dateTimePicker_DiaHistorial_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonBuscar_Movimiento_Click(object sender, EventArgs e)
        {
            if (comboBoxMovimiento.Text != "")
            {
                if (comboBoxMovimiento.Text == "Alta" || comboBoxMovimiento.Text == "Merma" || comboBoxMovimiento.Text == "Cancelacion")
                {
                    ClassModels.Reportes.buscarMov(dateTimePicker_DiaHistorial, dataGridView_HistorialInventario, comboBoxMovimiento.Text);
                }
                else
                {
                    if (comboBoxMovimiento.Text == "Ajustes")
                    {
                        ClassModels.Reportes.buscarReportes(dateTimePicker_DiaHistorial, dataGridView_HistorialInventario);
                    }
                    else
                    {
                        ClassModels.Reportes.buscarMovSal(dateTimePicker_DiaHistorial, dataGridView_HistorialInventario);
                    }
                }
            }

        }


        #endregion

        //Codigo de ventas ####################################################
        #region

        private void button_Ventas_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            restablecerVentas();
            if (role != "Admin")
            {
                //button_Clientes.Enabled = true;
                //button_Ventas.Enabled = false;
                //button_Productos.Enabled = true;
                //button_Compras.Enabled = false;
                //button_Dpto.Enabled = false;
                //button_Config.Enabled = false;
                //button_Corte.Enabled = false;
                //buttonVentas_Dia.Enabled = false;
            }
            else
            {
                /*button_Ventas.Enabled = false;
                button_Clientes.Enabled = true;
                button_Productos.Enabled = true;
                button_Compras.Enabled = true;
                button_Dpto.Enabled = true;
                button_Config.Enabled = true;
                button_Corte.Enabled = true;
                buttonVentas_Dia.Enabled = true;*/
            }
            textBox_BuscarProductos.Focus();
        }

        private void button_Ventas_KeyPress(object sender, KeyPressEventArgs e)
        {
        }



        private void button_Ventas_KeyDown(object sender, KeyEventArgs e)
        {
        }


        private void textBox_BuscarProductos_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.numberKeyPress(e);
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (textBox_BuscarProductos.Text == "")
                {
                    label_MensajeVenta.Text = "Ingrese un codigo de barras";
                    label_MensajeVenta.ForeColor = Color.Red;
                }
                else
                {
                    var producto = ClassModels.venta.buscarProductos(textBox_BuscarProductos.Text);
                    if (producto.Count > 0)
                    {
                        if (producto[0].Existencia.Equals(0))
                        {
                            label_MensajeVenta.Text = "No hay productos en existencia";
                            label_MensajeVenta.ForeColor = Color.Red;
                        }
                        else
                        {
                            label_MensajeVenta.Text = "";
                            ClassModels.venta.guardarVentasTempo(textBox_BuscarProductos.Text, 0, caja, idUsuario);
                            ClassModels.venta.buscarVentaTempo(dataGridView_Ventas, 1, tamañoPagina);
                            //ClassModels.venta.importes(label_ImportesVentas, caja, idUsuario);
                        }

                    }
                    else
                    {
                        label_MensajeVenta.Text = "No existe este producto ";
                        label_MensajeVenta.ForeColor = Color.Red;
                    }
                }
                textBox_BuscarProductos.Text = "";
                textBox_BuscarProductos.Text = textBox_BuscarProductos.Text.Replace("\n", "");
                textBox_BuscarProductos.Focus();
            }
        }

        private void restablecerVentas()
        {
            paginas = 0;
            /*textBox_Pagos.Text = "";
            textBox_BuscarClienteVenta.Text = "";
            label_Deuda.Text = "$0.00";
            if (checkBox_Credito.Checked == false)
            {
                label_ReciboDeuda.Text = "$0.00";
                label_ReciboDeudaTotal.Text = "$0.00";
                label_Nombre.Text = "Nombre";
                label_ReciboDeudaAnterior.Text = "$0.00";
                label_ReciboUltimoPago.Text = "$0.00";
                label_ReciboFecha.Text = "--/--/--";
                label_MensajeCliente.Text = "";
            }*/

            ClassModels.venta.buscarVentaTempo(dataGridView_Ventas, 1, tamañoPagina);
            //ClassModels.venta.importes(label_ImportesVentas, caja, idUsuario);
            ClassModels.venta.buscarCliente(dataGridView_ClienteVenta, textBox_BuscarClienteVenta.Text);
            //new Paginador(dataGridView_Ventas, label_PaginaVenta, paginas, 0);
        }

        private void dataGridView_Ventas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_Ventas.Rows.Count != 0)
            {
                string codigo = Convert.ToString(dataGridView_Ventas.CurrentRow.Cells[1].Value);
                int cantidad = Convert.ToInt16(dataGridView_Ventas.CurrentRow.Cells[4].Value);
                ClassModels.venta.borrarVenta(codigo, cantidad, caja, idUsuario);
                //ClassModels.venta.importes(label_ImportesVentas, caja, idUsuario);
                ClassModels.venta.buscarVentaTempo(dataGridView_Ventas, 1, tamañoPagina);
            }
        }

        private void dataGridView_Ventas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
            }
            else
            {
                if (e.KeyChar == Convert.ToChar(Keys.Tab))
                {


                }
            }

        }

        private void textBox_Pagos_TextChanged(object sender, EventArgs e)
        {
            label_MensajeCliente.Text = "";
            //ClassModels.venta.pagosCliente(textBox_Pagos, label_SuCambio, label_Cambio, label47, checkBox_Credito);
            //ClassModels.venta.datosCliente(checkBox_Credito, textBox_Pagos, textBox_BuscarClienteVenta,
            //    dataGridView_ClienteVenta, labels);
        }

        private void textBox_Pagos_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ClassModels.evento.numberDecimalKeyPress(textBox_Pagos, e);
        }

        private void textBox_BuscarClienteVenta_TextChanged(object sender, EventArgs e)
        {
            label_MensajeCliente.Text = "";
            ClassModels.venta.buscarCliente(dataGridView_ClienteVenta, textBox_BuscarClienteVenta.Text);
        }

        private void checkBox_Credito_CheckedChanged(object sender, EventArgs e)
        {
            label_MensajeCliente.Text = "";
            //ClassModels.venta.datosCliente(checkBox_Credito, textBox_Pagos, textBox_BuscarClienteVenta,
              //  dataGridView_ClienteVenta, labels);

            //ClassModels.venta.pagosCliente(textBox_Pagos, label_SuCambio, label_Cambio, label47, checkBox_Credito);
        }

        private void dataGridView_ClienteVenta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_ClienteVenta.Rows.Count != 0)
            {
                label_MensajeCliente.Text = "";
               /* if (checkBox_Credito.Checked == true)
                {
                    if (textBox_Pagos.Text != "")
                    {
                        ClassModels.venta.datosCliente(checkBox_Credito, textBox_Pagos, textBox_BuscarClienteVenta,
                            dataGridView_ClienteVenta, labels);
                    }
                    else
                    {
                        label_MensajeCliente.Text = "Ingrese el pago";
                        textBox_Pagos.Focus();
                    }
                }
                else
                {
                    label_MensajeCliente.Text = "Seleccione la opcion de credito";
                }
                */
            }
        }

        private void button_Cobrar_Click(object sender, EventArgs e)
        {
            /*pagocon = textBox_Pagos.Text;
            labelPagoConAnterior.Text = textBox_Pagos.Text;
            sucambio = label_Cambio.Text;
            labelTotalAnterior.Text = "$" + label_ImportesVentas.Text;
            labelCambioAnterior.Text = label_Cambio.Text;
            bool valor = ClassModels.venta.cobrar(checkBox_Credito, textBox_Pagos, dataGridView_ClienteVenta, labels, caja,
                idUsuario);
            if (valor)
            {
                groupBox = null;
                tipo = "venta";
                dateTimePicker = null;
                printDocument1.Print();
                restablecerVentas();
            }*/
            textBox_BuscarProductos.Focus();
        }

        private void button_VtPrimero_Click(object sender, EventArgs e)
        {
            //new Paginador(dataGridView_Ventas, label_PaginaVenta, paginas, 1).primero();
        }

        private void button_VtAnterior_Click(object sender, EventArgs e)
        {
            //new Paginador(dataGridView_Ventas, label_PaginaVenta, paginas, 1).anterior();
        }

        private void button_VtSiguiente_Click(object sender, EventArgs e)
        {
            //new Paginador(dataGridView_Ventas, label_PaginaVenta, paginas, 1).siguiente();
        }

        private void button_VtUltimo_Click(object sender, EventArgs e)
        {
           // new Paginador(dataGridView_Ventas, label_PaginaVenta, paginas, 1).ultimo();
        }
        private void button_ReciboVenta_Click(object sender, EventArgs e)
        {
           // groupBox = groupBox_ReciboVenta;
            printDocument1.Print();
            //checkBox_Credito.Checked = false;
            restablecerVentas();
        }


        private void button_CancelarVenta_Click(object sender, EventArgs e)
        {
            //labelPagoConAnterior.Text = textBox_Pagos.Text;
            //labelTotalAnterior.Text = "$" + label_ImportesVentas.Text;
            //labelCambioAnterior.Text = label_Cambio.Text;
            //bool valor = ClassModels.venta.cobrar(checkBox_Credito, textBox_Pagos, dataGridView_ClienteVenta, labels, caja,
                //idUsuario);
            //if (valor)
            //{
            //    groupBox = null;
            //    tipo = "venta_sin_ticket";
            //    dateTimePicker = null;
            //    printDocument1.Print();
            //    restablecerVentas();
            //}
            textBox_BuscarProductos.Focus();
        }



        #endregion


        //Codigo de busqueda en ventana ventas ################################
        #region

        private void button_BuscarProducto_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 9;
            textBox_buscarProductoNo.Focus();
            if (role != "Admin")
            {
                //button_Clientes.Enabled = true;
                //button_Ventas.Enabled = true;
                //button_Productos.Enabled = true;
                //button_Compras.Enabled = false;
                //button_Dpto.Enabled = false;
                //button_Config.Enabled = false;
                //button_Corte.Enabled = false;
                //buttonVentas_Dia.Enabled = false;
            }
            ClassModels.Products.buscarProductoVentas(dataGridView_BuscarProductoNo, "", 1, tamañoPagina);
        }

        private void textBox_buscarProductoNo_TextChanged(object sender, EventArgs e)
        {
            ClassModels.Products.buscarProductoVentas(dataGridView_BuscarProductoNo, textBox_buscarProductoNo.Text, 1, tamañoPagina);
        }

        private void dataGridView_BuscarProductoNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (dataGridView_BuscarProductoNo.Rows.Count != 0)
                {
                    string codigo = Convert.ToString(dataGridView_BuscarProductoNo.CurrentRow.Cells[1].Value);
                    int cantidad = Convert.ToInt16(dataGridView_BuscarProductoNo.CurrentRow.Cells[4].Value);
                    int exis = Convert.ToInt16(dataGridView_BuscarProductoNo.CurrentRow.Cells[5].Value);
                    if (exis > 0)
                    {
                        ClassModels.venta.guardarVentasTempo(codigo, 0, caja, idUsuario);
                        //ClassModels.venta.importes(label_ImportesVentas, caja, idUsuario);
                        ClassModels.venta.buscarVentaTempo(dataGridView_Ventas, 1, tamañoPagina);
                        tabControl1.SelectedIndex = 0;
                        if (role != "Admin")
                        {
                            //button_Clientes.Enabled = true;
                            //button_Ventas.Enabled = true;
                            //button_Productos.Enabled = true;
                            //button_Compras.Enabled = false;
                            //button_Dpto.Enabled = false;
                            //button_Config.Enabled = false;
                            //button_Corte.Enabled = false;
                            //buttonVentas_Dia.Enabled = false;
                        }
                        else
                        {
                            //button_Clientes.Enabled = true;
                            //button_Ventas.Enabled = true;
                            //button_Productos.Enabled = true;
                            //button_Compras.Enabled = true;
                            //button_Dpto.Enabled = true;
                            //button_Config.Enabled = true;
                        }
                        textBox_buscarProductoNo.Text = "";
                        labelMensaje_Buscar.Text = "";
                    }
                    else
                    {
                        labelMensaje_Buscar.Text = "No hay mas productos en existencia";
                        labelMensaje_Buscar.ForeColor = Color.Red;
                    }

                }

            }
        }

        private void dataGridView_BuscarProductoNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
        }


        #endregion

        //Codigo de CONFIGURACION #############################################
        #region

        private void button_Config_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 5;
            //button_Clientes.Enabled = true;
            //button_Ventas.Enabled = true;
            //button_Productos.Enabled = true;
            //button_Compras.Enabled = true;
            //button_Dpto.Enabled = true;
            //button_Config.Enabled = false;
        }

        #endregion

        //Codigo de USUARIOS ##################################################
        #region

        private void buttonUsuarios_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 10;
            var usuario = new Usuario(textBoxObject, labelsObject, comboBoxUser_Rol, dataGridView_User);
            usuario.restablecerUsuarios();
            buttonUsuarios.Enabled = false;
            buttonArticulos.Enabled = true;
            buttonEntradas.Enabled = true;
            buttonSalidas.Enabled = true;
            buttonReportes.Enabled = true;
            buttonPersonal.Enabled = true;
        }

        private void button_Usuarios_Click(object sender, EventArgs e)
        {
            
        }

        private void textBoxUser_Nombre_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUser_Nombre.Text == "")
            {
                labelUser_Nombre.ForeColor = Color.Red;
            }
            else
            {
                labelUser_Nombre.Text = "Nombre";
                labelUser_Nombre.ForeColor = Color.DarkCyan;
            }
        }

        private void textBoxUser_Nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.textKeyPress(e);
        }

        private void textBoxUser_Apellido_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUser_Apellido.Text == "")
            {
                labelUser_Apellido.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelUser_Apellido.Text = "Apellido";
                labelUser_Apellido.ForeColor = Color.DarkCyan;
            }
        }

        private void textBoxUser_Apellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.textKeyPress(e);
        }
        private void textBoxUser_Telefono_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUser_Telefono.Text == "")
            {
                labelUser_Telefono.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelUser_Telefono.Text = "Telefono";
                labelUser_Telefono.ForeColor = Color.DarkCyan;
            }
        }

        private void textBoxUser_Telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.numberKeyPress(e);
        }

        private void textBoxUser_Direccion_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUser_Direccion.Text == "")
            {
                labelUser_Direccion.ForeColor = Color.LightSlateGray;

            }
            else
            {
                labelUser_Direccion.Text = "Direccion";
                labelUser_Direccion.ForeColor = Color.DarkCyan;
            }
        }

        private void textBoxUser_Direccion_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBoxUser_Correo_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUser_Correo.Text == "")
            {
                labelUser_Correo.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelUser_Correo.Text = "Email";
                labelUser_Correo.ForeColor = Color.DarkCyan;
            }
        }

        private void textBoxUser_Correo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBoxUser_Contraseña_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUser_Contraseña.Text == "")
            {
                labelUser_Contraseña.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelUser_Contraseña.Text = "Password";
                labelUser_Contraseña.ForeColor = Color.DarkCyan;
            }
        }


        private void textBoxUser_Usuario_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUser_Usuario.Text == "")
            {
                labelUser_Usuario.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelUser_Usuario.Text = "Usuario";
                labelUser_Usuario.ForeColor = Color.DarkCyan;
            }
        }

        private void buttonUser_Guardar_Click(object sender, EventArgs e)
        {
            var usuario = new Usuario(textBoxObject, labelsObject, comboBoxUser_Rol, dataGridView_User);

            if (usuario.registrarUsuario())
            {
                usuario.restablecerUsuarios();
            }

        }

        private void buttonEliminar_Usuario_Click(object sender, EventArgs e)
        {
            if (idusuarioeliminar >= 0)
            {
                ClassModels.usuario.eliminarUsuario(idusuarioeliminar);
                idusuarioeliminar = -1;
                var usuario = new Usuario(textBoxObject, labelsObject, comboBoxUser_Rol, dataGridView_User);
                usuario.restablecerUsuarios();
            }

        }

        private void buttonUser_Cancelar_Click(object sender, EventArgs e)
        {
            var usuario = new Usuario(textBoxObject, labelsObject, comboBoxUser_Rol, dataGridView_User);
            usuario.restablecerUsuarios();
        }

        private void dataGridView_User_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_User.Rows.Count != 0)
            {
                var usuario = new Usuario(textBoxObject, labelsObject, comboBoxUser_Rol, dataGridView_User);
                usuario.dataGridViewUsuarios();
                idusuarioeliminar = Convert.ToInt16(dataGridView_User.CurrentRow.Cells[0].Value);
            }
        }

        private void dataGridView_User_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView_User.Rows.Count != 0)
            {
                var usuario = new Usuario(textBoxObject, labelsObject, comboBoxUser_Rol, dataGridView_User);
                usuario.dataGridViewUsuarios();
                idusuarioeliminar = Convert.ToInt16(dataGridView_User.CurrentRow.Cells[0].Value);
            }
        }

        private void textBoxUser_Buscar_TextChanged(object sender, EventArgs e)
        {
            ClassModels.usuario.searchUsuarios(dataGridView_User, textBoxUser_Buscar.Text, 1, tamañoPagina);
        }

        private void tabControlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlUser.SelectedIndex == 1)
            {
                ClassModels.usuario.Roles(listView_UserRoles);
            }
        }

        private void textBoxUser_Roles_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUser_Roles.Text == "")
            {
                labelUser_Roles.ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelUser_Roles.Text = "Role";
                labelUser_Roles.ForeColor = Color.DarkCyan;
            }
        }

        private void buttonUser_tabControl_Click(object sender, EventArgs e)
        {
            var usuario = new Usuario(textBoxObject, labelsObject, comboBoxUser_Rol, dataGridView_User);
            usuario.guardarRoles();
            ClassModels.usuario.Roles(listView_UserRoles);
        }

        private void listView_UserRoles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClassModels.usuario.deleteRoles(listView_UserRoles);
        }

        #endregion

        //Codigo de CAJAS #####################################################
        #region

        private Caja objectCaja;
        private List<Label> labelCajas;
        private List<TextBox> textBoxCajas;
        //private object[] objectCajas;

        private void button_Cajas_Click(object sender, EventArgs e)
        {
            paginas = 6;
            tabControl1.SelectedIndex = 6;
            objectCaja.restablecerCajas();
        }

        private void dateTimePicker_Cajas_ValueChanged(object sender, EventArgs e)
        {
            objectCaja.getIngresos();
        }

        private void dataGridView_CajasIngresos_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView_CajasIngresos.Rows.Count != 0)
            {
                objectCaja.dataGridViewCaja();
            }
        }

        private void dataGridView_CajasIngresos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_CajasIngresos.Rows.Count != 0)
            {
                objectCaja.dataGridViewCaja();
            }
        }

        private void textBox_Retirar_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.numberDecimalKeyPress(textBox_Retirar, e);
        }

        private void textBox_Retirar_TextChanged(object sender, EventArgs e)
        {
            objectCaja.cajaIngresos();
        }


        private void button_GuardarUser_Click(object sender, EventArgs e)
        {
            if (tabControl3.SelectedIndex.Equals(0))
            {
                if (objectCaja.cajaIngresos())
                {
                    objectCaja.guardarIngresos();
                }
            }
            else
            {
                if (textBoxCaja_IngresoInicial.Text.Equals(""))
                {
                    labelCaja_IngresoInicial.Text = "Ingrese el ingreso inicial";
                    labelCaja_IngresoInicial.ForeColor = Color.Red;
                }
                else
                {
                    objectCaja.insetarIngresoInicial();
                }
            }
        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl3.SelectedIndex.Equals(0))
            {
                tabControl4.SelectedIndex = 0;
                timer1.Stop();
            }
            else
            {
                tabControl4.SelectedIndex = 1;
                timer1.Start();
            }
        }

        private void tabControl4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl4.SelectedIndex.Equals(0))
            {
                tabControl3.SelectedIndex = 0;
                timer1.Stop();
            }
            else
            {
                tabControl3.SelectedIndex = 1;
                timer1.Start();
            }
        }

        private void dataGridViewCajas_Registros_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            string estado = (String)dataGridViewCajas_Registros.Rows[e.RowIndex].Cells[2].Value;
            if (estado.Equals("Activa"))
            {
                dataGridViewCajas_Registros.Rows[e.RowIndex].Cells[2].Style.ForeColor = Color.Red;
            }
            else
            {
                dataGridViewCajas_Registros.Rows[e.RowIndex].Cells[2].Style.ForeColor = Color.Green;
            }
        }

        private void dataGridViewCajas_Registros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            objectCaja.dataGridViewCajaIngresos();
        }

        private void dataGridViewCajas_Registros_KeyUp(object sender, KeyEventArgs e)
        {
            objectCaja.dataGridViewCajaIngresos();
        }

        private void textBoxCaja_IngresoInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.numberDecimalKeyPress(textBoxCaja_IngresoInicial, e);
        }

        private void button_CancelarUser_Click(object sender, EventArgs e)
        {
            objectCaja.restablecerCajas();
        }

        #endregion

        //Codigo de Inventario ################################################
        #region

        private List<Label> labelProductos;
        private List<TextBox> textBoxProductos;


        private Inventario inventario;

        private void button_Inventario_Click(object sender, EventArgs e)
        {
            paginas = 7;
            tabControl1.SelectedIndex = 7;
            inventario.restablecerInventario(labelCostoInventarioCompleto, labelGananciaAprox);
            ClassModels.promo.mostrarGrid("", dataGridViewPromos);
            ClassModels.promo.mostrarGridDos("", dataGridViewPromoDos);
        }

        private void dataGridView_Inventario_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int existencia = (int)dataGridView_Inventario.Rows[e.RowIndex].Cells[5].Value;
            int minimo = (int)dataGridView_Inventario.Rows[e.RowIndex].Cells[6].Value;
            if (existencia <= minimo)
            {
                dataGridView_Inventario.Rows[e.RowIndex].Cells[5].Style.ForeColor = Color.Red;
            }
            else
            {
                dataGridView_Inventario.Rows[e.RowIndex].Cells[5].Style.ForeColor = Color.Green;
            }
        }

        private void dataGridView_Inventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_Inventario.Rows.Count != 0)
            {
                textBoxPedidoDesc.Text = Convert.ToString(dataGridView_Inventario.CurrentRow.Cells[2].Value);
                textBoxPedidoCant.Text = Convert.ToString(dataGridView_Inventario.CurrentRow.Cells[5].Value);
                categoriapedido = Convert.ToString(dataGridView_Inventario.CurrentRow.Cells[8].Value);
                inventario.dataGridViewProductos();
                timer1.Stop();
            }
        }

        private void dataGridView_Inventario_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView_Inventario.Rows.Count != 0)
            {
                textBoxPedidoDesc.Text = Convert.ToString(dataGridView_Inventario.CurrentRow.Cells[2].Value);
                textBoxPedidoCant.Text = Convert.ToString(dataGridView_Inventario.CurrentRow.Cells[5].Value);
                categoriapedido = Convert.ToString(dataGridView_Inventario.CurrentRow.Cells[8].Value);
                inventario.dataGridViewProductos();
                timer1.Stop();
            }
        }

        private void buttonPedidoAgregar_Click(object sender, EventArgs e)
        {
            ClassModels.merma.insertarPedido(fecha, textBoxPedidoDesc.Text, Convert.ToInt32(textBoxPedidoCant.Text), categoriapedido);
            textBoxPedidoCant.Text = "";
            textBoxPedidoDesc.Text = "";
        }

        private void checkBoxInventario_Agotados_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void textBoxInventario_Existencia_TextChanged(object sender, EventArgs e)
        {
            if (textBoxInventario_Existencia.Text == "")
            {
                label52.ForeColor = Color.LightSlateGray;
            }
            else
            {
                label52.ForeColor = Color.DarkCyan;
            }
        }

        private void textBoxInventario_Existencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.numberKeyPress(e);
        }


        private void buttonGuardar_Inventario_Click(object sender, EventArgs e)
        {
            inventario.updateExistencia(labelCostoInventarioCompleto, labelGananciaAprox);
            timer1.Start();
        }

        private void textBoxInv_Buscar_TextChanged(object sender, EventArgs e)
        {
            int tab = tabControlInv.SelectedIndex;
            if (textBoxInv_Buscar.Text == "")
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
            switch (tab)
            {
                case 0:
                    inventario.getProducto(textBoxInv_Buscar.Text, 1, tamañoPagina, labelCostoInventarioCompleto, labelGananciaAprox);
                    break;
                case 1:
                    inventario.buscarVentas(textBoxInv_Buscar.Text, 1, tamañoPagina);
                    break;
            }
        }

        private void buttonExportar_Exel_Click(object sender, EventArgs e)
        {
            Type officeType = Type.GetTypeFromProgID("Excel.Application");
            if (officeType == null)
            {
                MessageBox.Show("Es necesario que tenga Excel instalado para poder exportar.", "Punto Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (dataGridView_Inventario.Rows.Count != 0)
                {
                    timer1.Stop();
                    String[] tutlulo = { "Codigo", "Descripcion", "Existencia", "Departamento", "Categoria" };
                    int[] column = { 0, 3, 4, 6, 9, 10 };
                    ExportData.exportarDataGridViewExcel(dataGridView_Inventario, tutlulo, column);
                }
            }

        }

        private void label82_Click(object sender, EventArgs e)
        {

        }

        private void groupBox33_Enter(object sender, EventArgs e)
        {

        }

        private void buttonExportar_PDF_Click(object sender, EventArgs e)
        {
            if (dataGridView_Inventario.Rows.Count != 0)
            {
                timer1.Stop();
                String[] tutlulo = { "Codigo", "Descripcion", "Existencia", "Departamento", "Categoria" };
                int[] column = { 0, 3, 4, 6, 9, 10 };
                ExportData.exportarDataGridViewPDF(dataGridView_Inventario, tutlulo, column);
            }
        }

        private void buttonInventario_Primero_Click(object sender, EventArgs e)
        {
            timer1.Start();
            new Paginador(dataGridView_Inventario, labelInventario_Paginas, paginas, 0).primero();
        }

        private void groupBox40_Enter(object sender, EventArgs e)
        {

        }

        private void label107_Click(object sender, EventArgs e)
        {

        }

        private void buttonInventario_Anterior_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            new Paginador(dataGridView_Inventario, labelInventario_Paginas, paginas, 1).anterior();
        }

        private void buttonInventario_Siguiente_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            new Paginador(dataGridView_Inventario, labelInventario_Paginas, paginas, 1).siguiente();
        }

        private void buttonInventario_Ultimo_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            new Paginador(dataGridView_Inventario, labelInventario_Paginas, paginas, 1).ultimo();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Start();
            inventario.restablecerInventario(labelCostoInventarioCompleto, labelGananciaAprox);
        }

        private void dateTimePickerInv_Ventas_ValueChanged(object sender, EventArgs e)
        {
            inventario.buscarVentas("", 1, 10000);
            //new Paginador(dataGridViewInv_Ventas, label71, 8, 0).primero();
        }

        private void dateTimePickerInv_Ventas2_ValueChanged(object sender, EventArgs e)
        {
            inventario.buscarVentas("", 1, 10000);
            //new Paginador(dataGridViewInv_Ventas, label71, 8, 0).primero();

        }

        private void checkBoxInv_MaxVentas_CheckedChanged(object sender, EventArgs e)
        {
            inventario.buscarVentas("", 1, 10000);
            //new Paginador(dataGridViewInv_Ventas, label71, 8, 0).primero();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //new Paginador(dataGridViewInv_Ventas, label71, 8, 0).primero();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //new Paginador(dataGridViewInv_Ventas, label71, 8, 1).anterior();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Paginador(dataGridViewInv_Ventas, label71, 8, 1).siguiente();
        }

        private void label_ImportesVentas_Click(object sender, EventArgs e)
        {

        }



        private void button2_Click(object sender, EventArgs e)
        {
            new Paginador(dataGridViewInv_Ventas, label71, 8, 1).ultimo();
        }



        private void buttonInv_Exel_Click(object sender, EventArgs e)
        {
            Type officeType = Type.GetTypeFromProgID("Excel.Application");
            if (officeType == null)
            {
                MessageBox.Show("Es necesario que tenga Excel instalado para poder exportar.", "Punto Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (dataGridViewInv_Ventas.Rows.Count != 0)
                {
                    String[] tutlulo = { "Codigo", "Descripcion", "Precio", "Cantidad", "importe", "Fecha", "Caja" };
                    int[] column = { 0, 6, 7, 8, 11 };
                    ExportData.exportarDataGridViewExcel(dataGridViewInv_Ventas, tutlulo, column);
                }
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F1)
            {
                tabControl1.SelectedIndex = 0;
                restablecerVentas();
                //button_Clientes.Enabled = true;
                //button_Ventas.Enabled = false;
                //button_Productos.Enabled = true;
                //button_Compras.Enabled = false;
                //button_Dpto.Enabled = false;
                //button_Config.Enabled = false;
                textBox_BuscarProductos.Focus();
            }

            if (e.KeyData == Keys.F3)
            {
                restablecer();
                //llamo a la ppagina 1
                tabControl1.SelectedIndex = 1;
                if (role != "Admin")
                {
                    radioButton_PagosDeudas.Checked = true;
                    radioButton_IngresarCliente.Enabled = false;
                    //button_Clientes.Enabled = false;
                    //button_Ventas.Enabled = true;
                    //button_Productos.Enabled = true;
                    //button_Dpto.Enabled = false;
                    //button_Compras.Enabled = false;
                    //button_Config.Enabled = false;
                    //button_Corte.Enabled = false;
                    button_EliminarClientes.Enabled = false;
                    button_Cancelar.Enabled = false;
                }
                else
                {
                    //button_Clientes.Enabled = false;
                    //button_Ventas.Enabled = true;
                    //button_Productos.Enabled = true;
                    //button_Dpto.Enabled = true;
                    //button_Compras.Enabled = true;
                    //button_Config.Enabled = true;
                    //button_Corte.Enabled = true;
                    button_EliminarClientes.Enabled = true;
                    button_Cancelar.Enabled = true;
                }
            }

            if (e.KeyData == Keys.F4)
            {
                tabControl1.SelectedIndex = 2;
                restablecerInvProd();
                comboBoxProvedor2.DataSource = ClassModels.Products.getCategoria();
                comboBoxProvedor2.DisplayMember = "Categoria";
                if (role != "Admin")
                {
                    //button_Clientes.Enabled = true;
                    //button_Ventas.Enabled = true;
                    //button_Productos.Enabled = false;
                    //button_Compras.Enabled = false;
                    //button_Dpto.Enabled = false;
                    //button_Config.Enabled = false;
                    //button_Corte.Enabled = false;
                }
                else
                {
                    //button_Productos.Enabled = false;
                }
                ClassModels.merma.mostrarGrid("", dataGridViewMerma);
                textBox_CoprasProductos.Focus();
            }

            if (e.KeyData == Keys.F5)
            {
                paginas = 12;
                tabControl1.SelectedIndex = 12;
                textBoxVariosCodigo.Focus();
            }

            if (e.KeyData == Keys.F12)
            {
                //textBox_Pagos.Focus();
            }

            if (e.KeyData == Keys.F2)
            {
                //pagocon = textBox_Pagos.Text;
                //labelPagoConAnterior.Text = "$" + textBox_Pagos.Text;
                //sucambio = label_Cambio.Text;
                //labelTotalAnterior.Text = label_ImportesVentas.Text;
                //labelCambioAnterior.Text = label_Cambio.Text;
                //bool valor = ClassModels.venta.cobrar(checkBox_Credito, textBox_Pagos, dataGridView_ClienteVenta, labels, caja,
                //    idUsuario);
                //if (valor)
                //{
                //    groupBox = null;
                //    tipo = "venta";
                //    dateTimePicker = null;
                //    printDocument1.Print();
                //    restablecerVentas();
                //}
                textBox_BuscarProductos.Focus();
            }

            if (e.KeyData == Keys.Subtract)
            {
                if (dataGridView_Ventas.Rows.Count != 0)
                {
                    string codigo = Convert.ToString(dataGridView_Ventas.CurrentRow.Cells[1].Value);
                    int cantidad = Convert.ToInt16(dataGridView_Ventas.CurrentRow.Cells[4].Value);
                    ClassModels.venta.borrarVenta(codigo, cantidad, caja, idUsuario);
                    //ClassModels.venta.importes(label_ImportesVentas, caja, idUsuario);
                    ClassModels.venta.buscarVentaTempo(dataGridView_Ventas, 1, tamañoPagina);
                }

            }

            if (e.KeyData == Keys.Add)
            {
                if (dataGridView_Ventas.Rows.Count != 0)
                {
                    //
                    string codigo = Convert.ToString(dataGridView_Ventas.CurrentRow.Cells[1].Value);
                    int cantidad = Convert.ToInt16(dataGridView_Ventas.CurrentRow.Cells[4].Value);
                    //renglonseleccionado = dataGridView_Ventas.SelectedRows[0].Index;
                    ClassModels.venta.agregarVenta(codigo, cantidad, caja, idUsuario);
                    //ClassModels.venta.importes(label_ImportesVentas, caja, idUsuario);
                    ClassModels.venta.buscarVentaTempo(dataGridView_Ventas, 1, tamañoPagina);
                    //if (renglonseleccionado >= 0) { dataGridView_Ventas.Rows[renglonseleccionado].Selected = true; }
                }
            }
        }

        private void dataGridView_Ventas_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void buttonInv_PDF_Click(object sender, EventArgs e)
        {
            if (dataGridView_Inventario.Rows.Count != 0)
            {
                String[] tutlulo = { "Codigo", "Descripcion", "Precio", "Cantidad", "importe", "Fecha", "Caja" };
                int[] column = { 0, 6, 7, 8, 11 };
                ExportData.exportarDataGridViewPDF(dataGridViewInv_Ventas, tutlulo, column);
            }
        }


        #endregion

        private void button7_Click(object sender, EventArgs e)
        {
            Salidas salidas = new Salidas(idUsuario, caja);
            salidas.Show();
        }

        private void buttonVentas_Dia_Click(object sender, EventArgs e)
        {
            TicketsVentas ticket = new TicketsVentas();
            ticket.Show();
        }

        //Codigo de Corte #####################################################
        #region


        private void button_Corte_Click(object sender, EventArgs e)
        {
            paginas = 11;
            tabControl1.SelectedIndex = 11;
        }

        private void buttonCorte_Cajero_Click(object sender, EventArgs e)
        {
            string usuario = comboBoxCorte_Cajero.Text;
            labelgrupCajero.Text = usuario;
            int iduser = ClassModels.usuario.IdUsuarioResult(usuario);
            ClassModels.Cortesitos.cortes(labelPago_Efectivo, labelEfectivo, labelVentas_Totales2, labelGanancias, labelEntradaInicial,
                labelEntradaTotal, labelEntrada, labelEfectivo_Total, labelUltima_Total, labelGrupFecha, labelgrupInicioCaja,
                labelgrupContado, labelGrupDineroCaja, labelGrupGanancia, labelGrupSalidas, labelSalidaCorte, labelSalidaCorteDos, dateTimePickerCorte, iduser, comboBoxCorte_Cajero.Text);
        }

        private void buttonImprimir_CorteDiario_Click(object sender, EventArgs e)
        {
            string usuario = comboBoxCorte_Cajero.Text;
            groupBox = null;
            dateTimePicker = dateTimePickerCorte;
            tipo = "cortediario";
            usuariocorte = usuario;
            printDocument1.Print();
        }

        private void buttonImprimir_CorteUsuario_Click(object sender, EventArgs e)
        {
            string usuario = comboBoxCorte_Cajero.Text;
            groupBox = null;
            dateTimePicker = dateTimePickerCorte;
            tipo = "corteusuario";
            usuariocorte = usuario;
            printDocument1.Print();
        }

        private void dataGridView_Inventario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView_Ventas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var ultimaFila = dataGridView_Ventas.Rows.Count - 1;
            if (ultimaFila >= 0) { dataGridView_Ventas.Rows[ultimaFila].Selected = true; }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string usuario = comboBoxCorte_Cajero.Text;
            labelgrupCajero.Text = "Todos";
            int iduser = ClassModels.usuario.IdUsuarioResult(usuario);
            ClassModels.Cortesitos.cortesGlobales(labelPago_Efectivo, labelEfectivo, labelVentas_Totales2, labelGanancias, labelEntradaInicial,
                labelEntradaTotal, labelEntrada, labelEfectivo_Total, labelUltima_Total, labelGrupFecha, labelgrupInicioCaja,
                labelgrupContado, labelGrupDineroCaja, labelGrupGanancia, labelGrupSalidas, labelSalidaCorte, labelSalidaCorteDos, dateTimePickerCorte, iduser);

        }

        private void buttonImprimir_Corte_Click(object sender, EventArgs e)
        {
            groupBox = groupBoxImprimir_Corte;
            printDocument1.Print();
        }

        #endregion

        //Codigo de Promociones ###############################################
        #region
        private void buttonGuardarPromo_Click(object sender, EventArgs e)
        {
            ClassModels.promo.insertarPromo(textBoxCodigo_Promo.Text, Convert.ToInt16(textBoxCantidad_Promo.Text),
                Convert.ToInt16(textBoxPrecio_Promo.Text), textBoxDescrip_Promo.Text);
            ClassModels.promo.mostrarGrid("", dataGridViewPromos);
            restablecerPromos();
        }

        private void dataGridViewPromos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxCodigo_Promo.Text = Convert.ToString(dataGridViewPromos.CurrentRow.Cells[1].Value);
            textBoxCantidad_Promo.Text = Convert.ToString(dataGridViewPromos.CurrentRow.Cells[2].Value);
            textBoxPrecio_Promo.Text = Convert.ToString(dataGridViewPromos.CurrentRow.Cells[3].Value);
            textBoxDescrip_Promo.Text = Convert.ToString(dataGridViewPromos.CurrentRow.Cells[4].Value);
        }

        private void dataGridViewPromos_KeyUp(object sender, KeyEventArgs e)
        {
            textBoxCodigo_Promo.Text = Convert.ToString(dataGridViewPromos.CurrentRow.Cells[1].Value);
            textBoxCantidad_Promo.Text = Convert.ToString(dataGridViewPromos.CurrentRow.Cells[2].Value);
            textBoxPrecio_Promo.Text = Convert.ToString(dataGridViewPromos.CurrentRow.Cells[3].Value);
            textBoxDescrip_Promo.Text = Convert.ToString(dataGridViewPromos.CurrentRow.Cells[4].Value);
        }

        private void buttonEliminarPromo_Click(object sender, EventArgs e)
        {
            ClassModels.promo.eliminarRegistro(textBoxCodigo_Promo.Text);
            ClassModels.promo.mostrarGrid("", dataGridViewPromos);
            restablecerPromos();
        }

        private void buttonCancelarPromo_Click(object sender, EventArgs e)
        {
            restablecerPromos();
        }

        private void restablecerPromos()
        {
            textBoxCodigo_Promo.Text = "";
            textBoxCantidad_Promo.Text = "";
            textBoxPrecio_Promo.Text = "";
            textBoxDescrip_Promo.Text = "";
        }
        // promociones segunda tabla #################################################################
        private void restablecerPromosDos()
        {
            textBoxCodigo_promoDos.Text = "";
            textBoxcantidad_PromoDos.Text = "";
            textBoxPrecio_PromoDos.Text = "";
            textBoxDesc_PromoDos.Text = "";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ClassModels.promo.insertarPromoDos(textBoxCodigo_promoDos.Text, Convert.ToInt16(textBoxcantidad_PromoDos.Text),
                Convert.ToInt16(textBoxPrecio_PromoDos.Text), textBoxDesc_PromoDos.Text, Convert.ToInt16(textBox_LimiteDos.Text));
            ClassModels.promo.mostrarGridDos("", dataGridViewPromoDos);
            restablecerPromosDos();
        }

        private void dataGridViewPromoDos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxCodigo_promoDos.Text = Convert.ToString(dataGridViewPromoDos.CurrentRow.Cells[1].Value);
            textBoxcantidad_PromoDos.Text = Convert.ToString(dataGridViewPromoDos.CurrentRow.Cells[2].Value);
            textBoxPrecio_PromoDos.Text = Convert.ToString(dataGridViewPromoDos.CurrentRow.Cells[3].Value);
            textBoxDesc_PromoDos.Text = Convert.ToString(dataGridViewPromoDos.CurrentRow.Cells[4].Value);
        }

        private void dataGridViewPromoDos_KeyUp(object sender, KeyEventArgs e)
        {
            textBoxCodigo_promoDos.Text = Convert.ToString(dataGridViewPromoDos.CurrentRow.Cells[1].Value);
            textBoxcantidad_PromoDos.Text = Convert.ToString(dataGridViewPromoDos.CurrentRow.Cells[2].Value);
            textBoxPrecio_PromoDos.Text = Convert.ToString(dataGridViewPromoDos.CurrentRow.Cells[3].Value);
            textBoxDesc_PromoDos.Text = Convert.ToString(dataGridViewPromoDos.CurrentRow.Cells[4].Value);
        }

        private void buttonEliminarPromoDos_Click(object sender, EventArgs e)
        {
            ClassModels.promo.eliminarRegistroDos(textBoxCodigo_promoDos.Text);
            ClassModels.promo.mostrarGridDos("", dataGridViewPromoDos);
            restablecerPromosDos();
        }

        private void buttonCancelarPromoDos_Click(object sender, EventArgs e)
        {
            restablecerPromosDos();
        }

        #endregion

        //Codigo de Merma #####################################################
        #region
        private void buttonAgregar_Merma_Click(object sender, EventArgs e)
        {
            ClassModels.merma.insertarMerma(textBoxCodigo_Merma.Text, Convert.ToInt32(textBoxCantidadMermar.Text), fecha, hora, usuario);
            ClassModels.merma.mostrarGrid("", dataGridViewMerma);
        }

        private void dataGridViewMerma_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxCodigo_Merma.Text = Convert.ToString(dataGridViewMerma.CurrentRow.Cells[1].Value);
            textBoxCantidadMermar.Text = Convert.ToString(dataGridViewMerma.CurrentRow.Cells[3].Value);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ClassModels.merma.eliminarRegistro(textBoxCodigo_Merma.Text);
            ClassModels.merma.mostrarGrid("", dataGridViewMerma);
        }

        private void buttonVerPedidios_Click(object sender, EventArgs e)
        {
            if (buttonVerPedidios.Text == "Ver pedidos")
            {
                panel3.Show();
                buttonVerPedidios.Text = "Ocultar Pedidos";
            }
            else
            {
                if (buttonVerPedidios.Text == "Ocultar Pedidos")
                {
                    panel3.Hide();
                    buttonVerPedidios.Text = "Ver pedidos";
                }
            }
        }

        private void buttonBuscar_Pedido_Click(object sender, EventArgs e)
        {
            ClassModels.merma.mostrarGridPedido(dateTimePickerPedido, dataGridViewPedidos, comboBoxProvedor2.Text);
        }

        private void dataGridViewPedidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contadorPagina = Convert.ToInt16(dataGridViewPedidos.CurrentRow.Cells[0].Value);
        }

        private void buttonEliminar_Pedido_Click(object sender, EventArgs e)
        {
            ClassModels.merma.eliminarPedido(contadorPagina);
        }
        #endregion

        //Codigo de productos varios ##########################################
        #region

        private void buttonF5Varios_Click(object sender, EventArgs e)
        {
            paginas = 12;
            tabControl1.SelectedIndex = 12;
            textBoxVariosCodigo.Focus();
        }

        private void buttonVarios_Click(object sender, EventArgs e)
        {
                if (textBoxVariosCant.Text == "" || textBoxVariosCodigo.Text == "")
                {
                    labelVariosMensaje.Text = "Ingrese un codigo de barras o cantidad";
                }
                else
                {
                    var producto = ClassModels.venta.buscarProductos(textBoxVariosCodigo.Text);
                    if (producto.Count > 0)
                    {
                        if (producto[0].Existencia.Equals(0))
                        {
                            labelVariosMensaje.Text = "No hay productos en existencia";
                        }
                        else
                        {
                            if (producto[0].Existencia >= Convert.ToInt16(textBoxVariosCant.Text))
                            {
                                
                            }
                            else
                            {
                                labelVariosMensaje.Text = "No hay productos suficientes en inventario";
                            }
                        }

                    }
                    else
                    {
                        labelVariosMensaje.Text = "No existe este producto ";
                    }
                }
        }

        private void textBoxVariosCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                textBoxVariosCant.Focus();
            }
        }

        private void textBoxVariosCant_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.numberKeyPress(e);
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (textBoxVariosCant.Text == "" || textBoxVariosCodigo.Text == "")
                {
                    labelVariosMensaje.Text = "Ingrese un codigo de barras o cantidad";
                }
                else
                {
                    var producto = ClassModels.venta.buscarProductos(textBoxVariosCodigo.Text);
                    if (producto.Count > 0)
                    {
                        if (producto[0].Existencia.Equals(0))
                        {
                            labelVariosMensaje.Text = "No hay productos en existencia";
                        }
                        else
                        {
                            if (producto[0].Existencia >= Convert.ToInt16(textBoxVariosCant.Text))
                            {
                                
                            }
                            else
                            {
                                labelVariosMensaje.Text = "No hay productos suficientes en inventario";
                            }
                        }

                    }
                    else
                    {
                        labelVariosMensaje.Text = "No existe este producto ";
                    }
                }
            }
        }

        #endregion

        //Codigo de Articulos #################################################
        #region
        private void buttonArticulos_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
            RestablecerProductos();
            buttonArticulos.Enabled = false;
            buttonEntradas.Enabled = true;
            buttonSalidas.Enabled = true;
            buttonReportes.Enabled = true;
            buttonUsuarios.Enabled = true;
            buttonPersonal.Enabled = true;
            //radioButton_Dpto.ForeColor = Color.DarkCyan;
            //textBox_Cat.ReadOnly = true;
        }

        private void RestablecerProductos()
        {
            IdProducto = 0;
            paginas = 4;
            accion = "insert";
            textBox_CodigoBarras.Text = "";
            textBox_DescripCompra.Text = "";
            textBox_PrecioVenta.Text = "";
            textBox_Hay.Text = "";
            label_CodigoBarras.Text = "Codigo de barras";
            label_CodigoBarras.ForeColor = Color.LightSlateGray;
            label_DescpCompra.Text = "Descripcion";
            label_DescpCompra.ForeColor = Color.LightSlateGray;
            label_PrecioVenta.Text = "Precio";
            label_PrecioVenta.ForeColor = Color.LightSlateGray;
            label_Existencia.Text = "Existencia";
            label_Existencia.ForeColor = Color.LightSlateGray;
            ClassModels.Products.buscarProducto(dataGridView_ComprasProductos, "", 1, tamañoPagina);
        }

        private void button_GuardarCompras_Click(object sender, EventArgs e)
        {
            decimal comprobarCosto = 0, comprobarPrecioventa = 0;
            comprobarPrecioventa = Convert.ToDecimal(textBox_PrecioVenta.Text);
            guardarProducto();
        }

        private void textBox_Hay_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.numberKeyPress(e);
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                decimal comprobarCosto = 0, comprobarPrecioventa = 0;
                comprobarPrecioventa = Convert.ToDecimal(textBox_PrecioVenta.Text);
                guardarProducto();

            }
        }

        private void guardarProducto()
        {
            if (textBox_CodigoBarras.Text == "")
            {
                label_CodigoBarras.Text = "Ingresar el codigo de barras";
                label_CodigoBarras.ForeColor = Color.Red;
                textBox_CodigoBarras.Focus();
            }
            else
            {
                if (textBox_DescripCompra.Text == "")
                {
                    label_DescpCompra.Text = "Ingresar una descripcion";
                    label_DescpCompra.ForeColor = Color.Red;
                    textBox_DescripCompra.Focus();
                }
                else
                {

                    if (textBox_PrecioVenta.Text == "")
                    {
                        label_PrecioVenta.Text = "Ingrese el precio de venta";
                        label_PrecioVenta.ForeColor = Color.Red;
                        textBox_PrecioVenta.Focus();
                    }
                    else
                    {
                        if (textBox_Hay.Text == "")
                        {
                            label_Existencia.Text = "Ingrese la existencia";
                            label_Existencia.ForeColor = Color.Red;
                            textBox_Hay.Focus();
                        }
                        else
                        {
                            string cod = textBox_CodigoBarras.Text;
                            string des = textBox_DescripCompra.Text;
                            decimal precioVent = Convert.ToDecimal(textBox_PrecioVenta.Text);
                            int exist = Convert.ToInt16(textBox_Hay.Text);
                            switch (accion)
                            {
                                case "insert":
                                    ClassModels.Products.guardarProducto(cod, des, precioVent, exist);
                                    RestablecerProductos();
                                    break;
                                case "update":
                                    ClassModels.Products.actualizarProducto(IdProducto, cod, des, precioVent, exist);
                                    RestablecerProductos();
                                    break;
                            }
                        }
                    }

                }
            }
        }

        private void textBox_BuscarCompras_TextChanged(object sender, EventArgs e)
        {
            ClassModels.Products.buscarProducto(dataGridView_ComprasProductos, textBox_BuscarCompras.Text, 1, tamañoPagina);
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView_ComprasProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_ComprasProductos.Rows.Count != 0)
                dataGridviewProductos();
        }

        private void dataGridView_ComprasProductos_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView_ComprasProductos.Rows.Count != 0)
                dataGridviewProductos();
        }

        private void dataGridviewProductos()
        {
            accion = "update";
            IdProducto = Convert.ToInt16(dataGridView_ComprasProductos.CurrentRow.Cells[0].Value);
            textBox_CodigoBarras.Text = Convert.ToString(dataGridView_ComprasProductos.CurrentRow.Cells[1].Value);
            textBox_DescripCompra.Text = Convert.ToString(dataGridView_ComprasProductos.CurrentRow.Cells[2].Value);
            textBox_Hay.Text = Convert.ToString(dataGridView_ComprasProductos.CurrentRow.Cells[3].Value);
            textBox_PrecioVenta.Text = Convert.ToString(dataGridView_ComprasProductos.CurrentRow.Cells[4].Value);
        }

        private void button_EliminarCompras_Click(object sender, EventArgs e)
        {
            if (IdProducto > 0)
            {
                if (MessageBox.Show("Estas seguro de eliminar este producto?", "Eliminar producto",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ClassModels.Products.borrarProducto(IdProducto, fecha);
                    RestablecerProductos();
                }
            }
        }

        private void button_CancelarCompras_Click(object sender, EventArgs e)
        {
            RestablecerProductos();
        }
        #endregion

        //Codigo de salidas ###################################################
        #region
        private void buttonSalidas_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            //restablecerDptoCat();
            buttonSalidas.Enabled = false;
            buttonArticulos.Enabled = true;
            buttonEntradas.Enabled = true;
            buttonReportes.Enabled = true;
            buttonUsuarios.Enabled = true;
            buttonPersonal.Enabled = true;
        }
        #endregion

        //Codigo de Personal ##################################################
        #region
        private void buttonPersonal_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 5;
            //RestablecerProductos();
            buttonPersonal.Enabled = false;
            buttonArticulos.Enabled = true;
            buttonEntradas.Enabled = true;
            buttonSalidas.Enabled = true;
            buttonReportes.Enabled = true;
            buttonUsuarios.Enabled = true;     
        }

        private void guardarCliente()
        {
            if (textBoxIDPersonal.Text == "")
            {
                label_Id.Text = "Ingrese el ID";
                label_Id.ForeColor = Color.Red;
                textBoxIDPersonal.Focus();
            }
            else
            {
                if (textBoxNombrePersonal.Text == "")
                {
                    label_Nombre.Text = "Ingrese el Nombre";
                    label_Nombre.ForeColor = Color.Red;
                    textBoxNombrePersonal.Focus();
                }
                else
                {
                    if (textBoxApellidoPersonal.Text == "")
                    {
                        label_Apellido.Text = "Ingrese el apellido";
                        label_Apellido.ForeColor = Color.Red;
                        textBoxApellidoPersonal.Focus();
                    }
                    else
                    {
                        if (textBoxDireccionPersonal.Text == "")
                        {
                            label_Direccion.Text = "Ingrese la direccion";
                            label_Direccion.ForeColor = Color.Red;
                            textBoxDireccionPersonal.Focus();
                        }
                        else
                        {
                                string ID = textBoxIDPersonal.Text;
                                string Nombre = textBoxNombrePersonal.Text;
                                string Apellido = textBoxApellidoPersonal.Text;
                                string Telefono = textBoxDireccionPersonal.Text;

                                if (accion == "insert")
                                {
                                    ClassModels.cliente.insertarCliente(ID, Nombre, Apellido, Telefono);
                                }
                                if (accion == "update")
                                {
                                    ClassModels.cliente.updateCliente(ID, Nombre, Apellido, Telefono, idCliente);
                                }
                                restablecer();
                            
                        }
                    }
                }
            }
        }

        private void restablecer()
        {
            paginas = 1;
            ClassModels.cliente.buscarCliente(dataGridViewPersonal, "", 1,1);
            textBoxIDPersonal.Text = "";
            textBoxNombrePersonal.Text = "";
            textBoxApellidoPersonal.Text = "";
            textBoxDireccionPersonal.Text = "";
            textBox_Id.Focus();
            textBox_BuscarCliente.Text = "";
            label_Id.ForeColor = Color.SlateGray;
            label_Nombre.ForeColor = Color.SlateGray;
            label_Apellido.ForeColor = Color.SlateGray;
            label_Direccion.ForeColor = Color.SlateGray;
            accion = "insert";
            idCliente = 0;
            IdRegistro = 0;
        }

        private void buttonGuardarPersonal_Click(object sender, EventArgs e)
        {
            guardarCliente();
        }
        #endregion
    }
}   
