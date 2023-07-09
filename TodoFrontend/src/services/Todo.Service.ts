import BaseEndpoint from "./BaseEndpoint.Service"

const TodoServices = {
  GetMany: BaseEndpoint.BaseUrl + 'v1/todos',
  GetOne: (id: number) => BaseEndpoint.BaseUrl + `v1/todos/${id}`,
  Create: BaseEndpoint.BaseUrl + 'v1/todos',
  Update: (id: number) => BaseEndpoint.BaseUrl + `v1/todos/${id}`,
  Delete: (id: number) => BaseEndpoint.BaseUrl + `v1/todos/${id}`,
  DeleteAll: BaseEndpoint.BaseUrl + 'v1/todos/delete-all',
}

export default TodoServices
