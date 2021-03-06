﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class ObjectBehavior : MonoBehaviour
{

	Vector3 m_start;
	Vector3 m_target;
	bool m_targeted = false;

	// Use this for initialization
	void Start()
	{
		// Add triggers.
		EventTrigger trigger = GetComponent<EventTrigger>();

		// EventTrigger entry for PointerEnter.
		EventTrigger.Entry enterEntry = new EventTrigger.Entry();
		enterEntry.eventID = EventTriggerType.PointerEnter;
		enterEntry.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });

		// EventTrigger entry for PointerExit.
		EventTrigger.Entry exitEntry = new EventTrigger.Entry();
		exitEntry.eventID = EventTriggerType.PointerExit;
		exitEntry.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });

		// Add the entries to the Event Trigger.
		trigger.triggers.Add(enterEntry);
		trigger.triggers.Add(exitEntry);

		// Set the initial and target scales.
		m_start = transform.localScale;
		m_target = m_start * 1.3f;
	}

	// Update is called once per frame
	void Update()
	{
		// Figure out which scale to move towards.
		Vector3 target = m_start;
		if (m_targeted)
		{
			target = m_target;
		}

		// Move towards said scale.
		transform.localScale = Vector3.MoveTowards(transform.localScale, target, Time.deltaTime);
	}

	private void OnPointerEnter(PointerEventData data)
	{
		m_targeted = true;
	}

	private void OnPointerExit(PointerEventData data)
	{
		m_targeted = false;
	}
}