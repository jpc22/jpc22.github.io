using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGA
{
    public List<GameObject> population;
    public RoomEvo controller;

    // constructor variables
    List<GameObject> furnList;
    int popSize;
    int maxGenerations;
    float mutationChance;
    float crossoverChance;
    // delegate declarations
    delegate int FitnessFunc(List<GameObject> pop);
    delegate void CrossoverFunc();
    delegate void ParentFunc();
    delegate void SelectionFunc();
    delegate void RecombinationFunc();
    // delegate instances
    FitnessFunc evalFitness;
    CrossoverFunc crossover;
    ParentFunc selectParents;
    SelectionFunc selectSurvivors;
    RecombinationFunc recombine;
    // run-time variables
    public float avg_fit;


    public BasicGA(RoomEvo controlObj, List<GameObject> furn_List, int pop_Size, int max_generations, float mutation_Chance, float crossover_Chance)
    {
        controller = controlObj;
        furnList = furn_List;
        popSize = pop_Size;
        maxGenerations = max_generations;
        mutationChance = mutation_Chance;
        crossoverChance = crossover_Chance;
    }

    public void initialize()
    {
        // Initialize Pop
        
        // Evaluate Fitness

    }

    public void runGeneration()
    {
        // Select Parents

        // Recombine pairs of parents

        // Mutate offspring

        // Add to population

        // Select survivors

        // Evaluate
    }

    void evaluate()
    {

    }
}
