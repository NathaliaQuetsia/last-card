using Godot;
using System;
using System.Collections.Generic;

public class MaoView : Spatial
{
    public JogoView Jogo { get; set; }

    [Export]
    public float larguraCarta = 1.4f;

    [Export]
    public float maximoMao = 9;

    [Export]
    public float distanciaCartas = 0.001f;
    private List<CartaView> handCartas = new List<CartaView>();

    public void addCarta(CartaView carta)
    {
        handCartas.Add(carta);
        var novasPosicoes = calculaPosicoes();
        var tween = GetNode<Tween>("Tween");
        for (int i = 0; i < handCartas.Count; i++)
        {
            tween.InterpolateProperty(handCartas[i], "translation", handCartas[i].Translation, novasPosicoes[i], 0.5f, Tween.TransitionType.Sine, Tween.EaseType.InOut);
            tween.InterpolateProperty(handCartas[i], "rotation_degrees", handCartas[i].RotationDegrees, Vector3.Zero, 0.5f, Tween.TransitionType.Sine, Tween.EaseType.InOut);
        }
        tween.Start();
    }

    private List<Vector3> calculaPosicoes()
    {
        List<Vector3> novasPosicoes = new List<Vector3>();
        Vector3 posicaoInicial = this.Translation;
        float larguraTemp = larguraCarta;
        float nX;
        if (handCartas.Count < maximoMao)
        {
            nX = ((handCartas.Count * larguraTemp) / 2) * -1;
            nX = nX + (larguraTemp / 2);
        }
        else
        {
            nX = ((maximoMao * larguraTemp) / 2) * -1;
            nX = nX + (larguraTemp / 2);
            larguraTemp = (nX * 2 * -1) / (handCartas.Count - 1);
        }
        float dZ = 0;

        foreach (var carta in handCartas)
        {
            Vector3 p = posicaoInicial;
            p = p + new Vector3(nX, 0, dZ);
            novasPosicoes.Add(p);
            nX = nX + larguraTemp;
            dZ = dZ + distanciaCartas;
        }
        return novasPosicoes;
    }
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}