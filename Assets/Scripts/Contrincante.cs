﻿using UnityEngine;
using System.Collections;

public class Contrincante : MonoBehaviour {

	private int estado;
	private Animator animcontr;
	private NavMeshAgent navmes;
	private GameObject player;
	private float distancia;
	public int vidaContrincante;
	public int auxquitarvida;


	// Use this for initialization
	void Start () {
		estado = 0;
		animcontr = gameObject.GetComponentInChildren<Animator>();
		animcontr.SetBool ("WaitC", true);
		navmes = gameObject.GetComponent <NavMeshAgent>();
		player = GameObject.Find ("mummy_rig");
		distancia = 2;
		vidaContrincante = 50;
		auxquitarvida = 0;
	}

	// Update is called once per frame
	void Update () {
		//Animacion
		animAdmin ();
		if (estado != 3) {
			if (navmes.SetDestination (player.transform.position)) {
				float aux = Vector3.Distance (player.transform.position, gameObject.transform.position);
				if (distancia > aux) {
					estado = 2;
					if (auxquitarvida == 10) {
						player.GetComponent <PlayerMovement> ().QuitarVida ();
						int auxvida = player.GetComponent <PlayerMovement> ().GetVida ();
						auxquitarvida = 0;
					}
					auxquitarvida++;
				} else {
					estado = 1;
			
				}
			}
			if (vidaContrincante <= 0) {
				estado = 3;
				player.GetComponent <PlayerMovement> ().PonerVida ();
			}
		}
	}

	private void animAdmin(){
		if (estado == 0) {
			animcontr.SetBool ("WalkC", false);
			animcontr.SetBool ("WaitC", true);
			animcontr.SetBool ("AtackC", false);
		} else if (estado == 1) {
			animcontr.SetBool ("WalkC", true);
			animcontr.SetBool ("WaitC", false);
			animcontr.SetBool ("AtackC", false);
		} else if (estado == 2) {
			animcontr.SetBool ("WalkC", false);
			animcontr.SetBool ("WaitC", false);
			animcontr.SetBool ("AtackC", true);
		} else if (estado == 3) {
			animcontr.SetBool ("WalkC", false);
			animcontr.SetBool ("WaitC", false);
			animcontr.SetBool ("AtackC", false);
			animcontr.SetBool ("DieC", true);
		}
	}

	public void QuitarVidaContr(){
		vidaContrincante--;
	}

	public int GetVidaContr(){
		return vidaContrincante;
	}
}