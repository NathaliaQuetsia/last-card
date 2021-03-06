using project.src.models;
using Godot;
using System;
using System.Text.Json;
using System.Collections.Generic;
namespace project.src.factory
{
    public class CartaFactory
    {
        private PackedScene recurso;
        private Texture[] difuses;
        private Texture[] normals;
        private Color[] cores;
        private ShaderMaterial shaderFrente;

        public CartaFactory()
        {
            recurso = ResourceLoader.Load<PackedScene>("res://scene/Carta.tscn");
            difuses = carregaDifuses();
            normals = carregaNormals();
            cores = carregaCores();
            shaderFrente = carregaShaderFrente();
        }

        public CartaView carregaCarta(Carta carta)
        {
            CartaView loadCarta = recurso.Instance() as CartaView;
            var novoShader = shaderFrente.Duplicate(true) as ShaderMaterial;
            loadCarta.init(normals, difuses, cores, novoShader);
            loadCarta.Carta = carta;
            return loadCarta;
        }

        private Texture[] carregaDifuses()
        {
            Texture[] difuses = new Texture[Enum.GetNames(typeof(VALOR)).Length];
            foreach (VALOR valor in Enum.GetValues(typeof(VALOR)))
            {
                if (valor != VALOR.SEM_VALOR)
                {
                    difuses[(int)valor] = ResourceLoader.Load<Texture>($"res://assets/texture/numeros/padrao/{Enum.GetName(typeof(VALOR), valor).ToLower()}_d_c.png");

                }
            }
            return difuses;
        }
        private Texture[] carregaNormals()
        {
            Texture[] normals = new Texture[Enum.GetNames(typeof(VALOR)).Length];
            foreach (VALOR valor in Enum.GetValues(typeof(VALOR)))
            {
                if (valor != VALOR.SEM_VALOR)
                {
                    normals[(int)valor] = ResourceLoader.Load<Texture>($"res://assets/texture/numeros/padrao/{Enum.GetName(typeof(VALOR), valor).ToLower()}_n.png");

                }
            }
            return normals;
        }

        private Color[] carregaCores()
        {
            Color[] cores = new Color[5];
            File file = new File();
            file.Open("res://assets/material/carta-frente-padrao.json", File.ModeFlags.Read);
            string conteudo = file.GetAsText();
            file.Close();
            List<CartaCor> valores = JsonSerializer.Deserialize<List<CartaCor>>(conteudo);
            for (int i = 0; i < 5; i++)
            {
                cores[(int)valores[i].CorCarta] = new Color(valores[i].CorShader);
            }
            return cores;

        }

        private ShaderMaterial carregaShaderFrente()
        {
            ShaderMaterial shader = ResourceLoader.Load<ShaderMaterial>("res://assets/material/carta-frente-padrao.tres");
            return shader;
        }
    }
}