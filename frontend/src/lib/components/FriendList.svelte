<script lang="ts">
	let data = $props();
	const { lovedones } = data.lovedones;
	console.log(lovedones);
</script>

{#each lovedones as loved}
	<p>{loved.relationship}: {loved.loved_name} + {loved.last_hangout}</p>
{/each}
