using LinqToDB;
using Punto_de_ventas.Connection;
using Punto_de_ventas.models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_ventas.modelsclass
{
    public class Usuario : Conexion
    {
        private Caja caja = new Caja();
        private List<usuarios> listUsuarios, listUsuario;
        private List<Cajas> listCajas, listCaja;
        private ComboBox comboBoxUser_Roles;
        private string fecha = DateTime.Now.ToString("dd/MMM/yyy");
        private string hora = DateTime.Now.ToString("hh:mm:ss");
        private TextBox textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8;
        private Label label1, label2, label3, label4, label5, label6, label7, label8;
        private static int idUsuario, pageSize = 15;
        private static String accion = "insert";
        private DataGridView dataGridView;

        public Usuario()
        {
            listUsuario = new List<usuarios>();
            listCaja = new List<Cajas>();
        }
        public Usuario(object[] textBox, object[] labels, ComboBox comboBoxUser_Roles, DataGridView dataGridView)
        {

            this.comboBoxUser_Roles = comboBoxUser_Roles;
            this.dataGridView = dataGridView;
            textBox1 = (TextBox)textBox[0];
            textBox2 = (TextBox)textBox[1];
            textBox3 = (TextBox)textBox[2];
            textBox4 = (TextBox)textBox[3];
            textBox5 = (TextBox)textBox[4];
            textBox6 = (TextBox)textBox[5];
            textBox7 = (TextBox)textBox[6];
            textBox8 = (TextBox)textBox[7];
            label1 = (Label)labels[0];
            label2 = (Label)labels[1];
            label3 = (Label)labels[2];
            label4 = (Label)labels[3];
            label5 = (Label)labels[4];
            label6 = (Label)labels[5];
            label7 = (Label)labels[6];
            label8 = (Label)labels[7];
        }

        public List<usuarios> getUsuarios()
        {
            return Usuario.ToList();
        }

        public object[] login(string usuario, string pass)
        {
            listUsuario.Clear();
            listUsuarios = Usuario.Where(u => u.Usuario == usuario).ToList();
            if (0 < listUsuarios.Count)
            {
                String pas = Encriptar.DecryptData(listUsuarios[0].Contraseña, listUsuarios[0].Usuario);
                if (pas == pass)
                {
                    listUsuario = listUsuarios;
                    int idUsuario = listUsuarios[0].IdUsuario;
                    string nombre = listUsuarios[0].Nombre;
                    string apellido = listUsuarios[0].Apellido;
                    string user = listUsuarios[0].Usuario;
                    string role = listUsuarios[0].Rol;
                }
            }
            object[] objts = { listUsuario, listCaja };
            return objts;
        }

        internal bool registrarUsuario()
        {
            bool valor = false;
            if (textBox1.Text == "")
            {
                label1.Text = "Ingrese el nombre";
                label1.ForeColor = Color.Red;
                textBox1.Focus();
            }
            else
            {
                if (textBox2.Text == "")
                {
                    label2.Text = "Ingrese el Apellido";
                    label2.ForeColor = Color.Red;
                    textBox2.Focus();
                }
                else
                {
                    if (textBox3.Text == "")
                    {
                        label3.Text = "Ingrese el telefono";
                        label3.ForeColor = Color.Red;
                        textBox3.Focus();
                    }
                    else
                    {
                        if (textBox4.Text == "")
                        {
                            label4.Text = "Ingrese la direccion";
                            label4.ForeColor = Color.Red;
                            textBox4.Focus();
                        }
                        else
                        {
                            if (textBox5.Text == "")
                            {
                                label5.Text = "Ingrese un correo";
                                label5.ForeColor = Color.Red;
                                textBox5.Focus();
                            }
                            else
                            {
                                if (textBox6.Text == "")
                                {
                                    label6.Text = "Ingrese una contraseña";
                                    label6.ForeColor = Color.Red;
                                    textBox6.Focus();
                                }
                                else
                                {
                                    if (textBox7.Text == "")
                                    {
                                        label7.Text = "Ingrese un nombre de usuario";
                                        label7.ForeColor = Color.Red;
                                        textBox7.Focus();
                                    }
                                    else
                                    {
                                        if (ClassModels.evento.comprobarFormatoCorreo(textBox5.Text))
                                        {
                                            var listEmail = Usuario.Where(u => u.Correo.Equals(textBox5.Text)).ToList();
                                            var listUsuario = Usuario.Where(u => u.Usuario.Equals(textBox7.Text)).ToList();

                                            var list = listUsuario.Union(listEmail).ToList();
                                            if (2 == list.Count)
                                            {
                                                if (idUsuario == listEmail[0].IdUsuario && idUsuario == listUsuario[0].IdUsuario)
                                                {
                                                    valor = true;
                                                    ejecutar();
                                                }
                                                else
                                                {
                                                    if (idUsuario != listEmail[0].IdUsuario)
                                                    {
                                                        label5.Text = "El correo ya esta registrado";
                                                        label5.ForeColor = Color.Red;
                                                        textBox5.Focus();
                                                        valor = false;
                                                    }
                                                    if (idUsuario != listUsuario[0].IdUsuario)
                                                    {
                                                        label7.Text = "El usuario ya esta registrado";
                                                        label7.ForeColor = Color.Red;
                                                        textBox7.Focus();
                                                        valor = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (0 == list.Count)
                                                {
                                                    valor = true;
                                                    ejecutar();
                                                }
                                                else
                                                {
                                                    if (0 != listEmail.Count)
                                                    {
                                                        if (idUsuario == listEmail[0].IdUsuario)
                                                        {
                                                            valor = true;
                                                        }
                                                        else
                                                        {
                                                            label5.Text = "El correo ya esta registrado";
                                                            label5.ForeColor = Color.Red;
                                                            textBox5.Focus();
                                                            valor = false;
                                                        }
                                                    }
                                                    if (0 != listUsuario.Count)
                                                    {
                                                        if (idUsuario == listUsuario[0].IdUsuario)
                                                        {
                                                            valor = true;
                                                        }
                                                        else
                                                        {
                                                            label7.Text = "El usuario ya esta registrado";
                                                            label7.ForeColor = Color.Red;
                                                            textBox7.Focus();
                                                            valor = false;
                                                        }
                                                    }
                                                    ejecutar();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            label5.Text = "El correo no es valido";
                                            label5.ForeColor = Color.Red;
                                            textBox5.Focus();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            void ejecutar()
            {
                if (valor == true)
                {
                    guardarUsuario();
                }
            }
            return valor;
        }
        private void guardarUsuario()
        {
            //var email = textBox5.Text;
            var pass = Encriptar.EncryptData(textBox6.Text, textBox7.Text);
            switch (accion)
            {
                case "insert":

                    Usuario.Value(u => u.Nombre, textBox1.Text)
                           .Value(u => u.Apellido, textBox2.Text)
                           .Value(u => u.Telefono, textBox3.Text)
                           .Value(u => u.Direccion, textBox4.Text)
                           .Value(u => u.Correo, textBox5.Text)
                           .Value(u => u.Contraseña, pass)
                           .Value(u => u.Usuario, textBox7.Text)
                           .Value(u => u.Rol, comboBoxUser_Roles.Text)
                           .Insert();

                    break;
                case "update":
                    Usuario.Where(u => u.IdUsuario.Equals(idUsuario))
                            .Set(u => u.Nombre, textBox1.Text)
                            .Set(u => u.Apellido, textBox2.Text)
                            .Set(u => u.Telefono, textBox3.Text)
                            .Set(u => u.Direccion, textBox4.Text)
                            .Set(u => u.Correo, textBox5.Text)
                            .Set(u => u.Contraseña, pass)
                            .Set(u => u.Usuario, textBox7.Text)
                            .Set(u => u.Rol, comboBoxUser_Roles.Text)
                            .Update();
                    break;
            }
        }

        public void eliminarUsuario(int idUsuario)
        {
            Usuario.Where(u => u.IdUsuario.Equals(idUsuario)).Delete();
        }

        public List<Roles> getRoles()
        {
            return roles.ToList();
        }

        public void searchUsuarios(DataGridView dataGridView, string campo, int num_pagina, int reg_por_pagina)
        {
            IEnumerable<usuarios> query;
            int inicio = (num_pagina - 1) * reg_por_pagina;
            if (campo.Equals(""))
            {
                query = Usuario.ToList();
            }
            else
            {
                query = Usuario.Where(p => p.Nombre.StartsWith(campo) || p.Apellido.StartsWith(campo) || p.Usuario.StartsWith(campo) || p.Correo.StartsWith(campo) || p.Telefono.StartsWith(campo));
            }
            dataGridView.DataSource = query.Skip(inicio).Take(reg_por_pagina).ToList();
            dataGridView.Columns[0].Visible = false;

            dataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView.Columns[7].Visible = false;
        }

        public void restablecerUsuarios()
        {
            idUsuario = 0;
            accion = "insert";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";

            label1.ForeColor = Color.LightSlateGray;
            label1.Text = "Nombre";
            label2.ForeColor = Color.LightSlateGray;
            label2.Text = "Apellido";
            label3.ForeColor = Color.LightSlateGray;
            label3.Text = "Telefono";
            label4.ForeColor = Color.LightSlateGray;
            label4.Text = "Direccion";
            label5.ForeColor = Color.LightSlateGray;
            label5.Text = "Email";
            label6.ForeColor = Color.LightSlateGray;
            label6.Text = "Password";
            label7.ForeColor = Color.LightSlateGray;
            label7.Text = "Usuario";
            label8.ForeColor = Color.LightSlateGray;
            label8.Text = "Role";

            comboBoxUser_Roles.DataSource = getRoles();
            comboBoxUser_Roles.ValueMember = "IdRol";
            comboBoxUser_Roles.DisplayMember = "Rol";
            searchUsuarios(dataGridView, "", 1, pageSize);
        }
        public void dataGridViewUsuarios()
        {
            accion = "update";
            idUsuario = Convert.ToInt16(dataGridView.CurrentRow.Cells[0].Value);
            textBox1.Text = Convert.ToString(dataGridView.CurrentRow.Cells[1].Value);
            textBox2.Text = Convert.ToString(dataGridView.CurrentRow.Cells[2].Value);
            textBox3.Text = Convert.ToString(dataGridView.CurrentRow.Cells[3].Value);
            textBox4.Text = Convert.ToString(dataGridView.CurrentRow.Cells[4].Value);
            textBox5.Text = Convert.ToString(dataGridView.CurrentRow.Cells[5].Value);
            String pass = Convert.ToString(dataGridView.CurrentRow.Cells[7].Value);
            textBox7.Text = Convert.ToString(dataGridView.CurrentRow.Cells[6].Value);
            textBox6.Text = Encriptar.DecryptData(pass, textBox7.Text);
            comboBoxUser_Roles.Text = Convert.ToString(dataGridView.CurrentRow.Cells[8].Value);
        }

        public void Roles(ListView list)
        {
            ListViewItem item;
            list.Items.Clear();
            foreach (var data in getRoles())
            {
                item = new ListViewItem(data.Rol, data.IdRol);
                //item.SubItems.Add(data.Role);
                list.Items.Add(item);
            }
        }
        public void guardarRoles()
        {
            if (textBox8.Text.Equals(""))
            {
                label8.Text = "Ingrese el role";
                label8.ForeColor = Color.Red;
                textBox8.Focus();
            }
            else
            {
                var roles1 = roles.Where(r => r.Rol.Equals(textBox8.Text)).ToList();
                if (roles1.Count.Equals(0))
                {
                    roles.Value(u => u.Rol, textBox8.Text)
                          .Insert();
                    restablecerUsuarios();
                }
                else
                {
                    label8.Text = "El rol ya esta registrado ";
                    label8.ForeColor = Color.Red;
                    textBox8.Focus();
                }
            }
        }
        public void deleteRoles(ListView list)
        {
            ListViewItem listItem = list.SelectedItems[0];
            roles.Where(r => r.IdRol.Equals(listItem.ImageIndex)).Delete();
            Roles(list);
        }

        public int IdUsuarioResult(string Usuarios)
        {
            int valor;
            var result = Usuario.Where(u => u.Usuario.Equals(Usuarios)).ToList();
            valor = result[0].IdUsuario;

            return valor;
        }

        public void listaTickets(DataGridView dataGridView, DateTimePicker dateTimePicker, int idUsuario, string numTicket)
        {
            IEnumerable<Ventas> query;
            var fecha_inicio = dateTimePicker.Value.Date.ToString("dd/MMM/yyy");
            int numTicket2;
            if (numTicket == "")
            {
                numTicket2 = 0;
            }
            else
            {
                numTicket2 = Convert.ToInt32(numTicket);
            }
            
            if (numTicket2 == 0)
            {
                 query = Ventas.Where(v => v.Fecha.Equals(fecha_inicio) && v.IdUsuario.Equals(idUsuario)).Distinct().ToList();
            }
            else
            {
                query = Ventas.Where(v => v.Fecha.Equals(fecha_inicio) && v.IdUsuario.Equals(idUsuario) && v.NumeroTicket.Equals(numTicket2)).Distinct().ToList();
            }
            dataGridView.DataSource = query.ToList();
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[1].Visible = false;
            dataGridView.Columns[2].Visible = false;
            dataGridView.Columns[3].Visible = false;
            dataGridView.Columns[4].Visible = false;
            dataGridView.Columns[5].Visible = false;
            dataGridView.Columns[6].Visible = false;
            dataGridView.Columns[7].Visible = false;
            dataGridView.Columns[8].Visible = false;
            dataGridView.Columns[9].Visible = false;
            dataGridView.Columns[10].Visible = false;
            dataGridView.Columns[11].Visible = false;
            dataGridView.Columns[13].Visible = false;
            dataGridView.Columns[15].Visible = false;
            dataGridView.Columns[16].Visible = false;
        }

        public void ventaXTicket(DataGridView dataGridView, string fecha, int caja, int idUsuario, int numTicket)
        {
            IEnumerable<Ventas> query;
                query = Ventas.Where(v => v.Fecha.Equals(fecha) && v.IdUsuario.Equals(idUsuario)
                && v.NumeroTicket.Equals(numTicket) && v.Caja.Equals(caja)).Distinct().ToList();
            dataGridView.DataSource = query.ToList();
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[1].Visible = false;
            dataGridView.Columns[3].Visible = false;
            dataGridView.Columns[6].Visible = false;
            dataGridView.Columns[7].Visible = false;
            dataGridView.Columns[8].Visible = false;
            dataGridView.Columns[9].Visible = false;
            dataGridView.Columns[10].Visible = false;
            dataGridView.Columns[11].Visible = false;
            dataGridView.Columns[12].Visible = false;
            dataGridView.Columns[13].Visible = false;
            dataGridView.Columns[14].Visible = false;
            dataGridView.Columns[15].Visible = false;
            dataGridView.Columns[16].Visible = false;
        }
        public void borrarVenta(int cant, int idVenta, string codigo)
        {
            int cantidad = 0, existencia = 0;
            var venta = Ventas.Where(t => t.IdVenta.Equals(idVenta)).ToList();
            cantidad = venta[0].Cantidad;
            var producto = Producto.Where(p => p.Codigo.Equals(codigo)).ToList();
            existencia = producto[0].Existencia;

            if (cantidad == 1)
            {
                existencia += cantidad;
                Ventas.Where(t => t.IdVenta.Equals(idVenta)).Delete();
            }
            else
            {
                existencia += cant;
                cantidad-= cant;
                if (cantidad >= 1)
                {
                    decimal proeli = Convert.ToDecimal(cant) * Convert.ToDecimal(venta[0].Precio.Replace("$",""));
                    decimal cantidadAct = Convert.ToDecimal(venta[0].Importe.Replace("$", "")) - proeli;
                    Ventas.Where(t => t.IdVenta.Equals(idVenta))
                    .Set(t => t.Cantidad, cantidad)
                    .Set(t => t.Importe, Convert.ToString("$"+cantidadAct))
                    .Update();
                }
                else
                {
                    Ventas.Where(t => t.IdVenta.Equals(idVenta)).Delete();
                }
                
            }
            Producto.Where(p => p.IdArticulo.Equals(producto[0].IdArticulo))
                        .Set(t => t.Existencia, existencia)
                        .Update();
            Movimientos.Value(m => m.Codigo, venta[0].Codigo)
                       .Value(m => m.Descripcion, venta[0].Descripcion)
                       .Value(m => m.Cantidad, cant)
                       .Value(m => m.TipoMovimiento, "Cancelacion")
                       .Value(m => m.Fecha, fecha)
                       .Value(m => m.Hora, hora)
                       .Value(m => m.Usuario, "Admin")
                       .Insert();
        }

        public void borrarVentaCompleta (string fecha, int idUsuario, int numTicket)
        {
            var ventas = Ventas.Where(t => t.Fecha.Equals(fecha) && t.IdUsuario.Equals(idUsuario) 
                                        && t.NumeroTicket.Equals(numTicket)).ToList();

            if (ventas.Count != 0)
            {
                ventas.ForEach(item =>
                {
                    var productos = Producto.Where(p => p.Codigo.Equals(item.Codigo)).ToList();
                    Producto.Where(p => p.Codigo.Equals(item.Codigo))
                            .Set(p => p.Existencia, productos[0].Existencia + item.Cantidad)
                            .Update();

                    Movimientos.Value(m => m.Codigo, item.Codigo)
                      .Value(m => m.Descripcion, item.Descripcion)
                      .Value(m => m.Cantidad, item.Cantidad)
                      .Value(m => m.TipoMovimiento, "Cancelacion")
                      .Value(m => m.Fecha, this.fecha)
                      .Value(m => m.Hora, hora)
                      .Value(m => m.Usuario, "Admin")
                      .Insert();
                });
                Ventas.Where(t => t.Fecha.Equals(fecha) && t.IdUsuario.Equals(idUsuario)
                               && t.NumeroTicket.Equals(numTicket)).Delete();
            }
        }
    }
}
