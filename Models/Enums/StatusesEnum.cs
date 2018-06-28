namespace Diss.Core.Enums
{
    public enum StatusesEnum
    {
        /// <summary>
        /// Ожидает решения ГМ
        /// </summary>
        WaitingModeration = 1, 
        /// <summary>
        /// Рассматривается координатором
        /// </summary>
        RewiewByCoordinator = 2,
        /// <summary>
        /// Формирование экспертной группы
        /// </summary>
        FormationOfAnExpertGroup = 3,
        /// <summary>
        /// Рассмотрение аналитиком
        /// </summary>
        ReviewByAnalists = 6,
        /// <summary>
        /// Завершена
        /// </summary>
        Completed = 8,
        /// <summary>
        /// Ожидает назначения аналитика и координатора
        /// </summary>
        WaitingForAppropriation = 10
    }
}
