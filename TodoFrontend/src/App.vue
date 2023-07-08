<template>
  <div v-if="!todos">Loading...</div>
  <div v-else class="h-screen">
    <header class="pr-4 pt-4 text-right w-full">
      <p class="text-xl text-gray-400">
        Made by
        <a class="text-gray-300" target="__blank" href="https://github.com/fabricioOak">fabricioOak</a>
      </p>
    </header>
    <main class="transition-all duration-200 w-full flex flex-col items-center pt-16" id="app">
      <div>
        <img src="../public/favicon.svg" alt="" />
        <h1 class="text-4xl capitalize mb-2">To do list</h1>
      </div>
      <p class="mb-8">What do you need to do?</p>
      <div class="flex flex-col justify-center items-center max-w-2xl sm:flex-row">

        <div class="w-80">
          <input
            class="focus:placeholder-transparent mb-2 w-80 border-white bg-gray-800 text-center rounded text-white text-2xl py-2 m-0 focus:outline-none"
            type="text" v-model="todoTitle" placeholder="Add a title" />
          <textarea
            class="focus:placeholder-transparent w-80 border-white bg-gray-800 text-center rounded text-white text-2xl py-2 m-0 focus:outline-none"
            type="text" v-model="todoContent" placeholder="Add the content" />
        </div>

        <div class="flex">
          <button @click="addTodo" @keyup.enter="addTodo"
            class="transition-all duration-200 button sm:mt-0 mt-4 text-white hover:bg-green-600 bg-green-400">
            Add
          </button>
          <!-- <button
            class="transition-all w-full duration-200 button sm:mt-0 mt-4 text-white hover:bg-yellow-600 bg- bg-yellow-500">
            Remove all
          </button> -->

        </div>
      </div>
      <div class="w-11/12 min-w-min max-w-2xl mt-8">
        <Todo v-for="(todo, index) in todos" :key="index" :id="todo.id" :title="todo.title" :content="todo.content"
          :done="todo.done" @updateState="updateTodoState" @delete="deleteTodo" />
      </div>
    </main>
  </div>
</template>

<script setup lang="ts">
import { useTodoStore } from "@/stores/TodoStore"
import { ref } from "vue";
import Todo from "./components/Todo.vue";

interface Todo {
  id: number
  title: string
  content: string
  done: boolean
}

const todoTitle = ref("");
const todoContent = ref("");
const store = useTodoStore();
const todos = ref<Todo[]>([]);

store.getMany().then(
  response => todos.value = response
)

const addTodo = async () => {

  if (!todoTitle.value || !todoContent.value) return;

  await store.create({
    title: todoTitle.value,
    content: todoContent.value,
  })

  todoTitle.value = "";
  todoContent.value = "";

  await store.getMany().then(
    response => todos.value = response
  )
}

const updateTodoState = async (id: number) => {
  await store.updateState(id)

  await store.getMany().then(
    response => todos.value = response
  )
}

const deleteTodo = async (id: number) => {
  await store.delete(id)

  await store.getMany().then(
    response => todos.value = response
  )
}

const newTodo = ref("");
</script>

<style lang="postcss">
.button {
  @apply appearance-none border-none outline-none font-bold text-black text-2xl py-2 px-4 rounded-lg ml-2 cursor-pointer;
}
</style>
