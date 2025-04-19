using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.CodeEditor;
using System;

public class TestThéo
{
    public float CalculateAnAverage(float[] value)
    {
        if (value == null || value.Length == 0)
        {
            throw new ArgumentException("Le tableu ne peut pas être vide");
        }
        float sum = 0;
        foreach (var v in value)
        {
            sum += v;
        }
        return sum / value.Length;
    }
     

    [Test]
    public void CalculateAveragePositive()
    {
        // Arrange
        float[] valeurs = { 10f, 20f, 30f };

        // Act
        float resultat = CalculateAnAverage(valeurs);

        // Assert
        Assert.GreaterOrEqual(resultat, 0f, "La moyenne ne doit pas être négative");
    }


    //Ce Test sert à montrer quand c'est pas bon
    [Test]
    public void CalculateAveragePositiveWithNegativeValue()
    {
        // Arrange
        float[] valeurs = { -10f, -5f, 0f };

        // Act
        float resultat = CalculateAnAverage(valeurs);

        // Assert
        Assert.GreaterOrEqual(resultat, 0f, "La moyenne ne doit pas être négative");
    }
}