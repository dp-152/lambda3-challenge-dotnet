interface IAPIResponse<T> {
  requestId: string,
  message?: string,
  errorDetails?: string,
  errorList?: string[],
  data: T,
}

export default IAPIResponse;
