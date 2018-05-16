﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles line drawing. Stores data for a line.
/// </summary>
[RequireComponent (typeof(SpriteRenderer))]
public class Line : MonoBehaviour
{
	/// <summary>
	/// Start position in world space. Calculates new line position when value changed.
	/// </summary>
	/// <value>The start position.</value>
	public Vector2 start {
		get { 
			return startPosition;
		}
		set {
			startPosition = value;
			UpdatePosition ();
		}
	}

	/// <summary>
	/// End position in world space. Calculates new line position when value changed.
	/// </summary>
	/// <value>The end position.</value>
	public Vector2 end {
		get { 
			return endPosition;
		}
		set {
			endPosition = value;
			UpdatePosition ();
		}
	}

	/// <summary>
	/// Width of line. Updates sprite renderer when value changed.
	/// </summary>
	/// <value>The width.</value>
	public float width {
		get { return lineWidth; }
		set {
			lineWidth = value;
			UpdateWidth ();
		}
	}

	/// <summary>
	/// Line color. Updates sprite renderer color when value changed.
	/// </summary>
	/// <value>The color.</value>
	public Color color {
		get { return lineColor; }
		set {
			lineColor = value;
			UpdateColor ();
		}
	}

	private Vector2 startPosition;
	private Vector2 endPosition;
	private float lineWidth;
	private Color lineColor;
	private SpriteRenderer lineRenderer;

	void Awake ()
	{
		lineRenderer = GetComponent<SpriteRenderer> ();
	}

	/// <summary>
	/// Initialise the specified start, end, width and color of sprite.
	/// </summary>
	/// <param name="start">Start position.</param>
	/// <param name="end">End position.</param>
	/// <param name="width">Width.</param>
	/// <param name="color">Color.</param>
	public void Initialise(Vector2 start, Vector2 end, float width, Color color)
	{
		startPosition = start;
		endPosition = end;
		lineWidth = width;
		lineColor = color;

		UpdatePosition ();
		UpdateWidth ();
		UpdateColor ();

        t = 0;
	}

	private void UpdatePosition ()
	{
		var heading = endPosition - startPosition;
		var distance = heading.magnitude;
		var direction = heading / distance;

		Vector3 centerPos = new Vector3 (startPosition.x + endPosition.x, startPosition.y + endPosition.y) / 2;
		lineRenderer.transform.position = centerPos;

		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
		lineRenderer.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);

		var objectWidthSize = 10f / 5f; // 10 = pixels of line sprite, 5f = pixels per units of line sprite.
		lineRenderer.transform.localScale = new Vector3 (distance / objectWidthSize, width, lineRenderer.transform.localScale.z);
	}

	private void UpdateWidth ()
	{
		lineRenderer.transform.localScale = lineRenderer.transform.localScale.WithY (lineWidth);
	}

	private void UpdateColor ()
	{
		lineRenderer.color = lineColor;
	}

    float t = 0;

    private void Update() {

        t += Time.deltaTime;
        if (t >= .2f) {
            this.gameObject.SetActive(false);
        }
    }
}
