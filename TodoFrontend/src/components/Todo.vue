<template>
  <div class="flex flex-col md:flex-row justify-between bg-gray-800 p-4 rounded-lg mb-4">
    <div class="todo">
      <div class="justify-normal">
        <h3 :class="`${props.done ? 'is-completed' : ''}`" class="text-2xl font-bold">{{ props.title }}:</h3>
      </div>
      <div :class="`${props.done ? 'is-completed' : ''}`" class="text-xl">
        {{ props.content }}
      </div>
    </div>
      <div class="flex md:mx-0 my-4 justify-end mx-auto">
        <button
          @click="emit('updateState', props.id)"
          class="button px-2 transiton-all w-full duration-200 hover:bg-green-600 bg-green-400"
        >
          {{ props.done ? "Undo" : "Done" }}
        </button>
        <button
        @click="emit('delete', props.id)"
          class="button px-2 transiton-all duration-200 w-full hover:bg-red-800 bg-red-600 text-white"
        >
          Delete
        </button>
      </div>

  </div>
</template>

<script setup lang="ts">

interface Todo {
  id: number
  title: string
  content: string
  done : boolean
}

const props = defineProps<Todo>();

const emit = defineEmits(['updateState', 'delete']);


</script>

<style lang="postcss" scoped>
.todo {
  @apply flex flex-col justify-between;
}
.button {
  @apply text-white text-lg;
  min-width: fit-content;
}

.is-completed {
  @apply text-gray-400 line-through opacity-50;
}
</style>
