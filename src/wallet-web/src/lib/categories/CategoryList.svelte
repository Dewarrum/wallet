<script lang="ts">
	import { ProgressBar } from '@skeletonlabs/skeleton';
	import { getCategories } from './queries';

	const categoryQuery = getCategories();
</script>

{#if $categoryQuery.isLoading}
	<ProgressBar />
{:else if $categoryQuery.data}
	{#each $categoryQuery.data.items as category}
		<div class="card mb-2">
			<header class="card-header">{category.name}</header>
			<footer class="card-footer">{category.description}</footer>
		</div>
	{/each}
{/if}
