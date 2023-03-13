// See https://aka.ms/new-console-template for more information



using System.Threading.Tasks;

Console.WriteLine("Usine de fabrication de gâteau.");
int nbGateauxPreparationMax = 3;
int nbGateauxCuissonMax = 5;
int nbGateauxEmballageMax = 2;
int nbGateauxTotalACuisiner = 10;
Random random = new Random();

FabriquerDesGateaux(nbGateauxTotalACuisiner, nbGateauxPreparationMax, nbGateauxCuissonMax, nbGateauxEmballageMax);

Console.WriteLine("\nPrograme terminé");
Console.ReadKey();


void FabriquerDesGateaux(int nbGateauxTotalACuisiner, int nbGateauxPreparationMax, int nbGateauxCuissonMax, int nbGateauxEmballageMax)
{

    int nbGateauxTermines = 0;
    int nbGateauxPreparation = 0;
    int nbGateauxCuisson = 0;
    int nbGateauxEmballage = 0;
    int indexDepartPrepa = 1;
    int indexDepartCuisson = 1;
    int indexDepartEmballage = 1;
    int nbTotalPrepa = nbGateauxTotalACuisiner;
    int nbTotalCuisson = nbGateauxTotalACuisiner;
    int nbTotalEmballage = nbGateauxTotalACuisiner;

    while (nbTotalPrepa > 0 || nbTotalCuisson > 0 || nbTotalEmballage > 0)
    {

        // Préparation
        List<Task> preparationTasks = new List<Task>();
        for (int i = 0; i < nbGateauxPreparationMax; i++)
        {
            if (nbTotalPrepa > 0)
            {
                nbGateauxPreparation = indexDepartPrepa;
                indexDepartPrepa++;
                Task task = Task.Run(() => PreparationDeGateaux(nbGateauxPreparation));
                preparationTasks.Add(task);
                nbTotalPrepa--;
            }
            else break;
        }

        // Cuisson
        List<Task> cuissonTasks = new List<Task>();
        for (int i = 0; i < nbGateauxCuissonMax && nbGateauxCuisson < nbGateauxPreparation; i++)
        {
            if(nbTotalCuisson > 0)
            {
                nbGateauxCuisson = indexDepartCuisson;
                indexDepartCuisson++;
                Task task = Task.Run(() => CuissonDeGateaux(nbGateauxCuisson));
                cuissonTasks.Add(task);
                nbTotalCuisson--;
            }
            else break;

        }

        // Emballage
        List<Task> emballageTasks = new List<Task>();
        for (int i = 0; i < nbGateauxEmballageMax && nbGateauxEmballage < nbGateauxCuisson; i++)
        {
            if (nbTotalEmballage > 0)
            {
                nbGateauxEmballage = indexDepartEmballage;
                indexDepartEmballage++;
                Task task = Task.Run(() =>
                {
                    EmballageDeGateaux(nbGateauxEmballage);
                    nbGateauxTermines++;
                });
                emballageTasks.Add(task);
                nbTotalEmballage--;
            }
            else break;

        }

        // Attente de la fin des tâches
        Task.WaitAll(preparationTasks.ToArray());
        Task.WaitAll(cuissonTasks.ToArray());
        Task.WaitAll(emballageTasks.ToArray());

        Releve(nbGateauxTermines, nbGateauxPreparation, nbGateauxCuisson, nbGateauxEmballage);
        Task.Delay(TimeSpan.FromSeconds(10)).Wait();

    }
}


void PreparationDeGateaux(int numeroDuGateau)
{
    Console.WriteLine("Préparation du gâteau #" + numeroDuGateau + " en cours...");
    int duration = random.Next(5, 8);
    Task.Delay(duration * 1000).Wait();
    Console.WriteLine("Préparation du gâteau #" + numeroDuGateau + " terminée.");
}

void CuissonDeGateaux(int numeroDuGateau)
{
    Console.WriteLine("Cuisson du gâteau #" + numeroDuGateau + " en cours...");
    Task.Delay(10000).Wait();
    Console.WriteLine("Cuisson du gâteau #" + numeroDuGateau + " terminée.");
}

void EmballageDeGateaux(int numeroDuGateau)
{
    Console.WriteLine("Emballage du gâteau #" + numeroDuGateau + " en cours...");
    Task.Delay(2000).Wait();
    Console.WriteLine("Emballage du gâteau #" + numeroDuGateau + " terminé.");
}

void Releve(int nbGateauxTermines, int nbGateauxPreparation, int nbGateauxCuisson, int nbGateauxEmballage)
{
    // Affichage des résultats
    Console.WriteLine();
    Console.WriteLine("#########################################");
    Console.WriteLine("RELEVE D'AFFICHAGE");
    Console.WriteLine("#########################################");
    Console.WriteLine("Nombre de gâteaux terminés : " + nbGateauxTermines);
    Console.WriteLine("Nombre de gâteaux en préparation : " + nbGateauxPreparation);
    Console.WriteLine("Nombre de gâteaux en cuisson : " + nbGateauxCuisson);
    Console.WriteLine("Nombre de gâteaux en emballage : " + nbGateauxEmballage);
    Console.WriteLine("#########################################");
    Console.WriteLine("#########################################");
    Console.WriteLine();

}