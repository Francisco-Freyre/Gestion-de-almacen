using LinqToDB;
using Punto_de_ventas.Connection;
using Punto_de_ventas.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_ventas.modelsclass
{
    public class Departamento : Conexion
    {
        public bool insertarDptoCat(string dptocat, int idDpto, string type)
        {
            bool valor = false;
            if (type == "dpto")
            {
                var departamento = Departamento.Where(d => d.Departamento == dptocat).ToList();
                if(0 == departamento.Count)
                {
                    Departamento.Value(d => d.Departamento, dptocat).Insert();
                    valor = true;
                }
                
            }
            else
            {
                var categoria = Categoria.Where(c => c.Categoria == dptocat).ToList();
                if (0 == categoria.Count)
                {
                    Categoria.Value(c => c.Categoria, dptocat).Value(c => c.IdDpto, idDpto).Insert();
                    valor = true;
                }
                
            }
            return valor;
        }
        public void buscarDpto(DataGridView dataGridView, string campo,int idDpto, int funcion)
        {
            switch (funcion)
            {
                case 1:
                    IEnumerable<Departamentos> query;
                    if (campo == "")
                        query = Departamento.ToList();
                    else
                        query = Departamento.Where(d => d.Departamento.StartsWith(campo));
                    dataGridView.DataSource = query.ToList();
                    dataGridView.Columns[0].Visible = false;
                    break;
                case 2:
                    IEnumerable<Categorias> query2;
                    query2 = Categoria.Where(c => c.IdDpto == idDpto).ToList();
                    dataGridView.DataSource = query2.ToList();
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    break;
            }
        }
        public bool actualizarDptoCat(string dptocat, int idDpto, int idCat, string type)
        {
            bool valor = false;
            if (type == "dpto")
            {
                var departamento = Departamento.Where(d => d.Departamento == dptocat).ToList();
                if (0 == departamento.Count || idDpto == departamento[0].IdDpto)
                {
                    Departamento.Where(d => d.IdDpto == idDpto)
                            .Set(d => d.Departamento, dptocat)
                            .Update();
                    valor = true;
                }

            }
            else
            {
                var categoria = Categoria.Where(c => c.Categoria == dptocat).ToList();
                if (0 == categoria.Count || idCat == categoria[0].IdCat)
                {
                    Categoria.Where(c => c.IdCat == idCat)
                            .Set(c => c.Categoria, dptocat)
                            .Set(c => c.IdDpto, idDpto)
                            .Update();
                    valor = true;
                }
                
            }
            return valor;
        }

        public void eliminarDptoCat(int idDpto, int idCat, string type)
        {
            if (type == "dpto")
            {
                Departamento.Where(d => d.IdDpto == idDpto).Delete();
                Categoria.Where(c => c.IdDpto == idDpto).Delete();
            }
            else
            {
                Categoria.Where(c => c.IdCat == idCat).Delete();
            }
        }
    }
}
